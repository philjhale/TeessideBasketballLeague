using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;

namespace Basketball.Service.Interfaces
{
    public interface IMatchResultService
    {
        void Commit();
        TeamLeague UpdateTeamLeagueStats(int teamLeagueId);
        void DeleteExistingPlayerFixturesThatDidNotPlay(List<PlayerFixture> playerFixtures, List<bool> hasPlayed);
        
        PlayerCareerStats UpdatePlayerCareerStats(PlayerFixture playerFixture);
        void UpdatePlayerCareerStats(List<PlayerFixture> playerFixtures);
        PlayerLeagueStats UpdatePlayerLeagueStats(PlayerFixture playerFixture, League league);
        void UpdatePlayerLeagueStats(List<PlayerFixture> playerFixtures, League league);
        PlayerSeasonStats UpdatePlayerSeasonStats(PlayerFixture playerFixture, Season season);
        void UpdatePlayerSeasonStats(List<PlayerFixture> playerFixtures, Season season);
        TeamLeague ResetStats(TeamLeague tl);
        void SaveMatchStats(bool hasPlayerStats, List<PlayerFixtureStats> homePlayerStats, TeamLeague teamLeague, Fixture fixture);

        Fixture SaveMatchResult(Fixture fixtureToUpdate, User lastUpdatedBy, int? forfeitingTeamId = null);
    }
}
