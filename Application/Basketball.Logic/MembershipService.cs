using System.Collections.Generic;
using System.Linq;
using Basketball.Common.Extensions;
using Basketball.Common.Util;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Service.Exceptions;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public class MembershipService : IMembershipService
    {
        private const string SiteAdminRole    = "Site Admin";
        private const string FixtureAdminRole = "Fixture Admin";
        private const string TeamAdminRole    = "Team Admin";

        readonly IMembershipRepository membershipRepository;
		readonly ISessionRepository sessionRepository;

        public MembershipService(IMembershipRepository membershipRepository,
			ISessionRepository sessionRepository)
        {
            this.membershipRepository = membershipRepository;
			this.sessionRepository = sessionRepository;
        }


        public void Commit()
        {
            membershipRepository.Commit();
        }
		
		public bool CanLogOn(string username, string password)
		{
			if(!ValidateUser(username, password))
				return false;
				
			sessionRepository.Roles(GetRolesForUser(username));
			
			return true;
		}

        #region Find Users
        public User GetUserByUserName(string username)
        {
            return membershipRepository.GetUserByUserName(username);
        }

        public User GetUser(int id)
        {
            return membershipRepository.GetUser(id);
        }
        #endregion

        #region User
        public bool ValidateUser(User user, string password)
        {
            if (user == null)
                return false;

            return user.Password == password.ToMd5();
        }

        public bool ValidateUser(string username, string password)
        {

            User user = membershipRepository.GetUserByUserName(username);

            return ValidateUser(user, password);
        }

        /// <exception cref="MatchResultZeroTeamScoreException"></exception>
        public void ChangePasswordForLoggedInUser(string currentPassword, string newPassword)
        {
            User user = membershipRepository.GetUserByUserName(sessionRepository.GetLoggedInUsername());

            // Currently no error checking at all. Am assuming the user is still logged in
            if (!ValidateUser(user, currentPassword))
                throw new ChangePasswordCurrentPasswordIncorrectException();

            // Assume everything is okay at this point
            user.Password = newPassword.ToMd5();
            SaveUser(user);
        }

        public string GetLoggedInUserName()
        {
            // Do I need to throw an exception it no user name is returned?
            return sessionRepository.GetLoggedInUsername();
        }

        public User GetLoggedInUser()
        {
            return GetUserByUserName(GetLoggedInUserName());
        }

        /// <exception cref="EmailSendException"></exception>
        public void ResetPassword(string username)
        {
            User user = membershipRepository.GetUserByUserName(username);

            // Don't dare if user doesn't exist
            if (user == null)
                return;
            
            // Don't really care how long the password is
            string randomPassword = Rand.String(8);

            user.Password = randomPassword.ToMd5();

            Email email = new Email(false, user.Email);

            // Send email BEFORE user is saved. Otherwise you could reset their password to something unknown
            // No error handling. Same old story. Can't be arse at the moment
            email.Send("TBL password reset", "Hello " + user.UserName + "\n\nYour new password is: " + randomPassword + "");
    
            SaveUser(user);
        }
        #endregion

        #region Role
        public bool IsUserInRole(string username, string roleName)
        {
            return membershipRepository.IsUserInRole(username, roleName);
        }

        public virtual List<string> GetRolesForUser(string username)
        {
            User user = GetUserByUserName(username);

            List<string> roles = new List<string>();

            if (user.SiteAdmin)
                roles.Add(SiteAdminRole);
            if (user.FixtureAdmin)
                roles.Add(FixtureAdminRole);
            if (user.TeamAdmin)
                roles.Add(TeamAdminRole);

            return roles;
        }


        /// <returns>Will always return a non null string</returns>
        public List<string> GetRolesForUserFromSession(string username)
        {
            return sessionRepository.Roles() ?? new List<string>();
        }

        public bool IsSiteAdmin(string username)
        {
            return GetRolesForUserFromSession(username).Contains(SiteAdminRole);
        }

        public bool IsFixtureAdmin(string username)
        {
            return GetRolesForUserFromSession(username).Contains(FixtureAdminRole);
        }

        public bool IsTeamAdmin(string username)
        {
            return GetRolesForUserFromSession(username).Contains(TeamAdminRole);
        }
        #endregion


        #region CRUD
        public void SaveUser(User user)
        {
            membershipRepository.InsertOrUpdateUser(user);
        }
        #endregion
    }
}
