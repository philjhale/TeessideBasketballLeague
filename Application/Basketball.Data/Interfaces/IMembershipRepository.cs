using System.Collections.Generic;
using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public interface IMembershipRepository
    {
        User GetUserByUserName(string username);
        User GetUser(int id);
        bool IsUserInRole(string username, string roleName);
        void InsertOrUpdateUser(User user);
        void Commit();
    }
}
