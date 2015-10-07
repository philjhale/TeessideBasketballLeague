using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public interface IMatchResultRepository
    {
        void Commit();

        void UpdateTeamLeague(TeamLeague newStats);
        void InsertOrUpdatePlayerFixture(PlayerFixture playerFixture);
        void DeletePlayerFixture(PlayerFixture playerFixture);

        PlayerCareerStats SavePlayerCareerStats(PlayerCareerStats playerCareerStats);
        PlayerLeagueStats SavePlayerLeagueStats(PlayerLeagueStats playerLeagueStats);
        PlayerSeasonStats SavePlayerSeasonStats(PlayerSeasonStats playerSeasonStats);
        PlayerCupStats SavePlayerCupStats(PlayerCupStats playerCupStats);
    }
}