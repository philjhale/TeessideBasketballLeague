using System;

namespace Basketball.Service.Exceptions
{
    #region Match Result
    public class MatchResultScoresSameException : Exception { }

    public class MatchResultZeroTeamScoreException : Exception { }

    public class MatchResultMaxPlayersExceededException : Exception { }

    public class MatchResultLessThanFivePlayersEachTeamException : Exception { }

    public class MatchResultSumOfScoresDoesNotMatchTotalException : Exception { }

    public class MatchResultNoMvpException : Exception { }

    public class MatchResultNoStatsMoreThanZeroPlayersException : Exception { } 
    #endregion

    #region Change Password
    public class ChangePasswordNewPasswordsDoNotMatchException : Exception { }

    public class ChangePasswordCurrentPasswordIncorrectException : Exception { }  
    #endregion

    #region Fixtures
    public class FixtureTeamsTheSameException : Exception { }

    public class FixtureRefereesTheSameException : Exception { } 
    #endregion
}
