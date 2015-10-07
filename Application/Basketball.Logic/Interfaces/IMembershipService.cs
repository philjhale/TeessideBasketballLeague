using System;
using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Service.Exceptions;

namespace Basketball.Service.Interfaces
{
    public interface IMembershipService
    {
        bool CanLogOn(string username, string password);
        User GetUser(int id);
        User GetUserByUserName(string username);
        bool IsUserInRole(string username, string roleName);
        List<string> GetRolesForUser(string username);

        /// <returns>Will always return a non null string</returns>
        List<string> GetRolesForUserFromSession(string username);

        /// <exception cref="MatchResultZeroTeamScoreException"></exception>
        void ChangePasswordForLoggedInUser(string currentPassword, string newPassword);
        string GetLoggedInUserName();
        void Commit();
        void ResetPassword(string username);
        User GetLoggedInUser();
        bool ValidateUser(User user, string password);
        bool ValidateUser(string username, string password);

        bool IsSiteAdmin(string username);
        bool IsFixtureAdmin(string username);
        bool IsTeamAdmin(string username);
    }
}
