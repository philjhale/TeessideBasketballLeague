using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes;
using System.Web.Security;
using System.Security.Cryptography;
using Basketball.Data.Interfaces;

namespace Basketball.Data
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly IUserRepository userRepository;

        public MembershipRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Commit()
        {
            userRepository.Commit();
        }
        
        #region User
        public User GetUserByUserName(string username)
        {
            return
                (from u in userRepository.Get()
                 where u.UserName.ToLower() == username.ToLower()
                 select u).SingleOrDefault();
        }

        public User GetUser(int id)
        {
            return
                (from u in userRepository.Get()
                 where u.Id == id
                 select u).Single();
        } 
        #endregion

        #region Role

        public bool IsUserInRole(string username, string roleName)
        {
            // No lowercase check. Problem?
            //return
            //    (from ur in userRoleRepository.GetQueryable()
            //     where ur.Role.Name == roleName
            //           && ur.User.UserName == username
            //     select ur).Any();
            throw new NotImplementedException();
        }

        #endregion

        #region CRUD
        public void InsertOrUpdateUser(User user)
        {
            userRepository.InsertOrUpdate(user);
        } 
        #endregion

    }
}
