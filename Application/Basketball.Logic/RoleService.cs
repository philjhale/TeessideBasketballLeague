using System;
using System.Web.Security;
using Basketball.Data.Interfaces;
using Ninject;

namespace Basketball.Service
{
    public class RoleService : RoleProvider
    {
        private readonly IMembershipRepository membershipRepository;

        public RoleService()
        {
            //membershipRepository = new StandardKernel().Get<IMembershipRepository>();
        }

        public RoleService(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return membershipRepository.IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return membershipRepository.GetRolesForUser(username);
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
