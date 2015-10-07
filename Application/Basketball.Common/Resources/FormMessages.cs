using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basketball.Common.Resources
{
    // TODO Move to resource file
    public class FormMessages
    {
        // Message types defined in master pages
        public const string MessageTypeSuccess = "successMessage";
        public const string MessageTypeFailure = "failureMessage";

        // Generic success/fail messages
        public const string DeleteConfirm = "Are you sure?";
        public const string DeleteSuccess = "Deleted successfully";
        public const string DeleteFail = "Delete has failed";
        public const string SaveSuccess = "Saved successfully";
        public const string SaveFail = "Save has failed";

        // Season related messages
        public const string SeasonCreateSuccess = "New season successfully created! Please now create the leagues for the new season";
        public const string SeasonCreateCurrentYearFail = "A new season cannot be created until next year";

        // Account messsages
        public const string AccountResetPassword = "If the username entered is valid, a new password will be emailed to the associated user. Please check your spam or junk folder";
        public const string AccountChangePassword = "Your password has been changed successfully";
        public const string AccountChangePasswordNewAndConfirmPasswordsDoNotMatch = "The new password and confirmation password do not match";
        public const string AccountChangePasswordCurrentPasswordIncorrect = "The current password you have entered is incorrect";
        public const string AccountLogOnFailed = "Either the user or password you have entered is incorrect";

        // My players
        public const string MyPlayersPlayerRemoved = "Player removed from team";

        // Form field errors
        public const string FieldMandatory = "Mandatory field";
        public const string FieldNumeric = "Field must be a number";
        public const string FieldNumericGreaterThanZero = "Field must be an number greater than zero";
        public const string FieldTooLong = "Field is too long";
        public const string FieldTwentyFourHour = "Field must be in a 24 hour format. e.g. 19:30";
        public const string FieldFixtureTeamsMatch = "Home and away teams are the same";
        public const string FieldFixtureRefsMatch = "Both referees are the same";
        public const string FieldFixtureCupOrLeague = "Please select either a cup OR a league";
        public const string FieldUrlInvalidFormat = "URL must start with http://";
        public const string FieldEmailInvalid = "Invalid email address";

        // Fixture messages
        public const string FixtureCancelled = "Fixture has been marked as cancelled. Please reschedule as soon as possible";
        public const string FixtureTeamsTheSame = "Home and away team cannot be the same";
        public const string FixtureRefereesTheSame = "Referees 1 and 2 cannot be the same";

        // Player messages
        public const string BatchPlayerCreateSuccess = "Multiple players created successfully";
        public const string BatchPlayerInvalidFields = "Enter both a forename and surname for each player you want to create";

        // Match result messages
        public const string MatchResultInvalidFoulRange = "Fouls must be between 0 and 5";
        public const string MatchResultMaxPlayersExceeded = "Only 12 players can play in each fixture for each team";
        public const string MatchResultSumScoreDoesNotMatch = "The sum of the players scores does not match the fixture score";
        public const string MatchResultScoresSame = "Home team and away team match scores cannot be the same";
        public const string MatchResultZeroTeamScore = "Home and away team scores must be greater than zero";
        public const string MatchResultFivePlayersEachTeam = "Five players must have played from each team";
        public const string MatchResultNoStatsZeroPlayers = "Include player stats is not ticked. Please remove all player stats";
        public const string MatchResultNoMvp = "Both home and away MVPs must be selected";

        // Feedback
        public const string FeedbackSendError = "Sorry there has been an error sending your message";
    }
}
