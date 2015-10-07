using Basketball.Domain.Entities;
using System.Collections.Generic;

namespace Basketball.Service.Interfaces
{
    public interface IStatsReportingService
    {
        PlayerFixture GetPlayerFixture(int id);

        List<PlayerFixture> GetPlayerFixturesForLeagueAndPlayer(int playerId, int leagueId);
        List<PlayerFixture> GetPlayerStatsForFixture(int fixtureId, int teamLeagueId);
        List<PlayerFixture> GetPlayerFixtureStatsForSeason(int playerId, int seasonId);
        List<PlayerFixture> GetPlayerFixtureStatusForAllSeasons(int playerId);
        List<PlayerFixture> GetPlayerFixtureStatsForCupAndSeason(int playerId, int cupId, int seasonId);

        PlayerLeagueStats GetPlayerLeagueStats(int playerId, int leagueId);
        PlayerCareerStats GetPlayerCareerStatsByPlayerId(int playerId);
        PlayerSeasonStats GetPlayerSeasonStats(int playerId, int seasonId);
        PlayerCupStats GetPlayerCupStats(int playerId, int cupId, int seasonId);
        List<PlayerSeasonStats> GetPlayerAllSeasonStats(int playerId);
        List<PlayerSeasonStats> GetPlayerSeasonStatsForTeamAndSeason(int teamId, int seasonId);

        List<PlayerLeagueStats> GetTopAvgScorersForLeague(int leagueId);
        List<PlayerLeagueStats> GetTopScorersForLeague(int leagueId);
        List<PlayerLeagueStats> GetTopAvgFoulersForLeague(int leagueId);
        List<PlayerLeagueStats> GetTopFoulersForLeague(int leagueId);
        List<PlayerLeagueStats> GetTopMvpAwardsForLeague(int leagueId);
        List<PlayerLeagueStats> GetTopAvgScorersForLeague(int leagueId, int numResults);

        List<PlayerCupStats> GetTopAvgScorersForCup(int cupId, int seasonId, int numResults);

        List<object> BuildStatsTicker();
        //void InsertOrUpdatePlayerFixturesThatHavePlayed(List<PlayerFixture> playerFixtures, List<bool> hasPlayed);
        List<PlayerFixture> GetTopPlayerStatsForFixture(int fixtureId, int teamLeagueId);
        List<LeagueWinner> GetLeagueWinsForTeam(int teamId);
        Fixture GetBiggestHomeWinForTeam(int teamId);
        Fixture GetBiggestAwayWinForTeam(int teamId);
        int GetTotalWins(int teamId);
        int GetTotalLosses(int teamId);
        int GetTotalPointsFor(int teamId);
        int GetTotalPointsAgainst(int teamId);
        decimal GetAveragePointsPerGameForTeamFor(int teamId);
        decimal GetAveragePointsPerGameForTeamAgainst(int teamId);
        List<LeagueWinner> GetPastLeagueWinners();

        List<CupWinner> GetCupWinsForTeam(int teamId);

        List<CupWinner> GetPastCupWinners();
    }
}
