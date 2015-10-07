using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;

namespace Basketball.Data
{
    public class MatchResultRepository : IMatchResultRepository
    {
        #region Init

        private readonly ITeamLeagueRepository teamLeagueRepository;
        private readonly IPlayerLeagueStatsRepository playerLeagueStatsRepository;
        private readonly IPlayerFixtureRepository playerFixtureRepository;
        private readonly IPlayerSeasonStatsRepository playerSeasonStatsRepository;
        private readonly IPlayerCareerStatsRepository playerCareerStatsRepository;
        private readonly IPlayerCupStatsRepository playerCupStatsRepository;

        public MatchResultRepository(ITeamLeagueRepository teamLeagueRepository,
            IPlayerLeagueStatsRepository playerLeagueStatsRepository,
            IPlayerFixtureRepository playerFixtureRepository,
            IPlayerSeasonStatsRepository playerSeasonStatsRepository,
            IPlayerCareerStatsRepository playerCareerStatsRepository,
            IPlayerCupStatsRepository playerCupStatsRepository)
        {
            this.teamLeagueRepository        = teamLeagueRepository;
            this.playerLeagueStatsRepository = playerLeagueStatsRepository;
            this.playerFixtureRepository     = playerFixtureRepository;
            this.playerSeasonStatsRepository = playerSeasonStatsRepository;
            this.playerCareerStatsRepository = playerCareerStatsRepository;
            this.playerCupStatsRepository    = playerCupStatsRepository;
        } 
        #endregion

        public void Commit()
        {
            teamLeagueRepository.Commit();
            playerLeagueStatsRepository.Commit();
            playerFixtureRepository.Commit();
            playerSeasonStatsRepository.Commit();
            playerCareerStatsRepository.Commit();
            playerCupStatsRepository.Commit();
        }

        #region CRUD
        public void UpdateTeamLeague(TeamLeague teamLeague)
        {
            teamLeagueRepository.Update(teamLeague);
        }

        

        public void InsertOrUpdatePlayerFixture(PlayerFixture playerFixture)
        {
            playerFixtureRepository.InsertOrUpdate(playerFixture);
        } 

        public void DeletePlayerFixture(PlayerFixture playerFixture)
        {
            playerFixtureRepository.Delete(playerFixture);
        }
        #endregion

        public PlayerCareerStats SavePlayerCareerStats(PlayerCareerStats playerCareerStats)
        {
            playerCareerStatsRepository.InsertOrUpdate(playerCareerStats);
            //Check.Require(!(playerCareerStats is IHasAssignedId<int>),
            //    "For better clarity and reliability, Entities with an assigned Id must call Save or Update");

            //Session.SaveOrUpdate(playerCareerStats);
            return playerCareerStats;
        }

        public PlayerLeagueStats SavePlayerLeagueStats(PlayerLeagueStats playerLeagueStats)
        {
            playerLeagueStatsRepository.InsertOrUpdate(playerLeagueStats);
            return playerLeagueStats;
        }

        public PlayerSeasonStats SavePlayerSeasonStats(PlayerSeasonStats playerSeasonStats)
        {
            playerSeasonStatsRepository.InsertOrUpdate(playerSeasonStats);
            return playerSeasonStats;
        }

        public PlayerCupStats SavePlayerCupStats(PlayerCupStats playerCupStats)
        {
            playerCupStatsRepository.InsertOrUpdate(playerCupStats);
            return playerCupStats;
        }
    }
}
