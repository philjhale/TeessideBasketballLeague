using System.Collections.Generic;
using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public interface IStatsReportingRepository
    {
        PlayerFixture GetPlayerFixture(int id);
        IQueryable<PlayerFixture> GetPlayerFixturesForLeagueAndPlayer(int playerId, int leagueId);
        IQueryable<PlayerFixture> GetPlayerFixtureStatsForSeason(int playerId, int seasonId);
        IQueryable<PlayerFixture> GetMvpForFixture(int fixtureId, int teamLeagueId);
        IQueryable<PlayerFixture> GetPlayerFixtureStatusForAllSeasons(int playerId);
        IQueryable<PlayerFixture> GetPlayerStatsForFixture(int fixtureId, int teamLeagueId);
        IQueryable<PlayerFixture> GetTopScorersForFixture(int fixtureId, int teamLeagueId, int numTopScorers);
        IQueryable<PlayerFixture> GetPlayerFixtureStatsForCupAndSeason(int playerId, int cupId, int seasonId);

        IQueryable<PlayerSeasonStats> GetPlayerAllSeasonStats(int playerId);
        PlayerSeasonStats GetPlayerSeasonStats(int id);
        PlayerSeasonStats GetPlayerSeasonStats(int playerId, int seasonId);
        IQueryable<PlayerSeasonStats> GetPlayerSeasonStatsForTeamAndSeason(int teamId, int seasonId);

        PlayerLeagueStats GetPlayerLeagueStats(int id);
        PlayerLeagueStats GetPlayerLeagueStats(int playerId, int leagueId);
        IQueryable<PlayerLeagueStats> GetTopAvgFoulersForLeague(int leagueId, int numResults);
        IQueryable<PlayerLeagueStats> GetTopAvgScorersForLeague(int leagueId, int numResults);
        IQueryable<PlayerLeagueStats> GetTopFoulersForLeague(int leagueId, int numResults);
        IQueryable<PlayerLeagueStats> GetTopMvpAwardsForLeague(int leagueId, int numResults);
        IQueryable<PlayerLeagueStats> GetTopScorersForLeague(int leagueId, int numResults);
        IQueryable<PlayerCupStats> GetTopAvgScorersForCup(int cupId, int seasonId, int numResults);

        PlayerCupStats GetPlayerCupStats(int playerId, int cupId, int seasonId);

        PlayerCareerStats GetPlayerCareerStatsByPlayerId(int playerId);
        
        IQueryable<LeagueWinner> GetLeagueWinsForTeam(int teamId);
        IQueryable<LeagueWinner> GetAllLeagueWinners();
        IQueryable<CupWinner> GetCupWinnersForTeam(int teamId);
        IQueryable<CupWinner> GetAllCupWinners();

        int? GetFixtureIdForBiggestedHomeWinForTeam(int teamId);
        int? GetFixtureIdForBiggestedAwayWinForTeam(int teamId);
        int GetTotalWins(int teamId);
        int GetTotalLosses(int teamId);
        int GetTotalPointsFor(int teamId);
        int GetTotalPointsAgainst(int teamId);
        int GetAveragePointsPerGameForTeam(int teamId);

        //void InsertOrUpdatePlayerFixturesThatHavePlayed(List<PlayerFixture> playerFixtures, List<bool> hasPlayed);
    }
}
