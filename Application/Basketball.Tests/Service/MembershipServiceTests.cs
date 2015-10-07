using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service;
using Basketball.Service.Interfaces;
using NUnit;
using NSubstitute;
using NUnit.Framework;

namespace Basketball.Tests.Service
{
    // Can't really do any testing because I need to mock the class under test, which is think is wrong
    [TestFixture]
    class MembershipServiceTests
    {
        IMembershipService membershipService;

        IMembershipRepository mockMembershipRepository;
        ISessionRepository mockSessionRepository;

        [SetUp]
        public void Setup()
        {
            mockMembershipRepository = Substitute.For<IMembershipRepository>();
            mockSessionRepository = Substitute.For<ISessionRepository>();

            membershipService = new MembershipService(mockMembershipRepository, mockSessionRepository);
        }

        [TearDown]
        public void TearDown()
        {
            membershipService = null;
        }

        #region Commit
        [Test]
        public void Commit_Success()
        {
            membershipService.Commit();

            mockMembershipRepository.Received().Commit();
        }
        #endregion

        #region CanLogOn
        [Ignore]
        [Test]
        public void CanLogOn_ValidLogOn_Success()
        {
            var roles = new List<string>() { "Admin" };
            membershipService.ValidateUser("myuser", Arg.Any<string>()).Returns(true);
            membershipService.GetRolesForUser("myuser").Returns(roles);

            Assert.That(membershipService.CanLogOn("user", "password"), Is.True);
            mockSessionRepository.Received().Roles(roles);
        } 
        #endregion

        #region GetUserByUserName
        [Test]
        public void GetUserByUserName_Success()
        {
            membershipService.GetUserByUserName("user1");

            mockMembershipRepository.Received().GetUserByUserName("user1");
        } 
        #endregion

        #region GetUser
        [Test]
        public void GetUser_Success()
        {
            membershipService.GetUser(123);

            mockMembershipRepository.Received().GetUser(123);
        }
        #endregion

        #region ValidateUser(User, string)
        [Test]
        public void ValidateUser_User_ValidDetails_ReturnTrue()
        {
            // Probably violating some unit testing principle here
            var returnValue = membershipService.ValidateUser(new User() { Password = "5f4dcc3b5aa765d61d8327deb882cf99" }, "password");

            Assert.That(returnValue, Is.True);
        }

        [Test]
        public void ValidateUser_User_InvalidDetails_ReturnTrue()
        {
            // Probably violating some unit testing principle here
            var returnValue = membershipService.ValidateUser(new User() { Password = "5f4dcc3b5aa765d61d8327deb882cf99" }, "bananas");

            Assert.That(returnValue, Is.False);
        }

        [Test]
        public void ValidateUser_User_NullUser_ReturnFalse()
        {
            var returnValue = membershipService.ValidateUser((User)null, "password");

            Assert.That(returnValue, Is.False);
        }
        #endregion

        #region ValidateUser(string, string)
        [Test]
        public void ValidateUser_ValidDetails_ReturnTrue()
        {
            var user = new User() { Password = "5f4dcc3b5aa765d61d8327deb882cf99" };
            mockMembershipRepository.GetUserByUserName("phil1").Returns(user);

            // Probably violating some unit testing principle here
            var returnValue = membershipService.ValidateUser("phil1", "password");

            Assert.That(returnValue, Is.True);
        }

        [Test]
        public void ValidateUser_InvalidDetails_ReturnTrue()
        {
            var user = new User() { Password = "5f4dcc3b5aa765d61d8327deb882cf99" };
            mockMembershipRepository.GetUserByUserName("phil1").Returns(user);

            // Probably violating some unit testing principle here
            var returnValue = membershipService.ValidateUser("phil1", "bananas");

            Assert.That(returnValue, Is.False);
        }

        [Test]
        public void ValidateUser_NullUser_ReturnFalse()
        {
            User user = null;
            mockMembershipRepository.GetUserByUserName("phil1").Returns(user);

            // Probably violating some unit testing principle here
            var returnValue = membershipService.ValidateUser("phil1", "password");

            Assert.That(returnValue, Is.False);
        }
        #endregion

        #region ChangePasswordForLoggedInUser
        [Test]
        [Ignore]
        public void ChangePasswordForLoggedInUser()
        {
            
        }
        #endregion

        #region GetLoggedInUserName
        [Test]
        public void GetLoggedInUserName_Success()
        {
            membershipService.GetLoggedInUserName();

            mockSessionRepository.Received().GetLoggedInUsername();
        }
        #endregion

        #region GetLoggedInUser
        [Test]
        public void GetLoggedInUser_Success()
        {
            membershipService.GetLoggedInUser();

            mockSessionRepository.Received().GetLoggedInUsername();
        }
        #endregion

        #region GetLoggedInUser
        [Test]
        public void IsUserInRole_UserInRole_ReturnTrue()
        {
            mockMembershipRepository.IsUserInRole("me", "admin").Returns(true);

            Assert.That(membershipService.IsUserInRole("me", "admin"), Is.True);
        }

        [Test]
        public void IsUserInRole_UserNotInRole_ReturnFalse()
        {
            mockMembershipRepository.IsUserInRole("me", "admin").Returns(false);

            Assert.That(membershipService.IsUserInRole("me", "admin"), Is.False);
        }
        #endregion

        #region GetLoggedInUser
        [Test]
        public void GetRolesForUser_HasRoles_ReturnRoleList()
        {
            User userWithRoles = new User() {SiteAdmin = true, TeamAdmin = true};
            mockMembershipRepository.GetUserByUserName("joe").Returns(userWithRoles);

            var returnedRoles = membershipService.GetRolesForUser("joe");

            Assert.That(returnedRoles, Is.Not.Null);
            Assert.That(returnedRoles.Count, Is.EqualTo(2));
            Assert.Contains("Site Admin", returnedRoles);
            Assert.Contains("Team Admin", returnedRoles);
        }

        [Test]
        public void GetRolesForUser_ZeroRoles_ReturnEmptyList()
        {
            User userWithZeroRoles = new User() { SiteAdmin = false, TeamAdmin = false };
            mockMembershipRepository.GetUserByUserName("joe").Returns(userWithZeroRoles);

            var returnedRoles = membershipService.GetRolesForUser("joe");

            Assert.That(returnedRoles, Is.Not.Null);
            Assert.That(returnedRoles.Count, Is.EqualTo(0));
        }
        #endregion


        #region GetRolesForUserFromSession
        [Test]
        public void GetRolesForUserFromSession_HasRoles_ReturnsNonEmptyList()
        {
            var roleList = new List<string>() {"Admin"};
            mockSessionRepository.Roles().Returns(roleList);

            var returnedRoles = membershipService.GetRolesForUserFromSession(Arg.Any<string>());

            Assert.That(returnedRoles, Is.Not.Null);
            Assert.That(returnedRoles.Count, Is.EqualTo(1));
            Assert.Contains("Admin", returnedRoles);
        }

        [Test]
        public void GetRolesForUserFromSession_ZeroRoles_ReturnsEmptyList()
        {
            List<string> roleList = null;
            mockSessionRepository.Roles().Returns(roleList);

            var returnedRoles = membershipService.GetRolesForUserFromSession(Arg.Any<string>());

            Assert.That(returnedRoles, Is.Not.Null);
            Assert.That(returnedRoles.Count, Is.EqualTo(0));
        }
        #endregion

        
    }
}
