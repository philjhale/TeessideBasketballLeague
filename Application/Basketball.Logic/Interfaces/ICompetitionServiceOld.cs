using System;
using Basketball.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Basketball.Service.Exceptions;

namespace Basketball.Service.Interfaces
{
    public interface ICompetitionService
    {
        Season CreateNextSeason(Basketball.Domain.Season currentSeason);
        Season GetCurrentSeason();
        List<Basketball.Domain.Fixture> GetLatestMatchResults();
        TeamLeague UpdateTeamLeagueStats(int teamLeagueId);
        List<League> GetLeaguesForSeason(int seasonId);
        List<League> GetLeaguesForCurrentSeason();
        List<TeamLeague> GetStandingsForLeague(int leagueId);
        Fixture GetFixture(int id);
        List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, string isCup);
        List<Fixture> GetFixturesForCurrentSeasonFilter();
        List<Team> GetTeamsForCurrentSeason();
        List<Season> GetSeasons();
        List<Season> GetSeasons(Expression<Func<Season, bool>> filter = null, Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null);
        List<Fixture> GetTeamHomeFixturesForCurrentSeason(int teamId);

        /// <exception cref="MatchResultMaxPlayersExceededException"></exception>
        /// <exception cref="MatchResultLessThanFivePlayersEachTeamException"></exception>
        /// <exception cref="MatchResultSumOfScoresDoesNotMatchTotalException"></exception>
        /// <exception cref="MatchResultNoMvpException"></exception>
        /// <exception cref="MatchResultNoStatsMoreThanZeroPlayersException"></exception>
        /// <exception cref="MatchResultScoresSameException"></exception>
        /// <exception cref="MatchResultZeroTeamScoreException"></exception>
        void SaveMatchResult(Fixture fixtureToUpdate,
                             List<PlayerFixture> homePlayerStats,
                             List<PlayerFixture> awayPlayerStats,
                             List<bool> homeHasPlayed,
                             List<bool> awayHasPlayed,
                             int homeMvpPlayerId,
                             int awayMvpPlayerId);
    }
}
