using System.Collections.Generic;
using System.ServiceModel;
using Basketball.Domain.Entities;
using Basketball.WebService.DataTransferObjects;

namespace Basketball.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITblDataService" in both code and config file together.
    [ServiceContract(Namespace = "http://www.teessidebasketball.co.uk")]
    public interface ITblDataService
    {
        [OperationContract]
        List<News> GetNews(int numberOfArticles);
        
        [OperationContract]
        List<FixtureDto> GetLatestMatchResults(int numberOfMatchResults);

        [OperationContract]
        MatchResultDto GetMatchResult(int fixtureId);

        /// <param name="teamId">0 for all teams</param>
        /// <param name="isPlayed">Y/N or null/blank for all teams</param>
        [OperationContract]
        List<FixtureDto> GetFixturesFiltered(int teamId, string isPlayed);
        
        [OperationContract]
        List<Team> GetTeamsForCurrentSeason();

        [OperationContract]
        List<TeamNameDto> GetTeamsNamesForCurrentSeason();

        [OperationContract]
        LeagueStandingsDto GetStandingsForCurrentSeason();

        [OperationContract]
        Team GetTeam(int id);

        [OperationContract]
        PlayerStatsDto GetAllPlayerStats(int id);

        [OperationContract]
        void TestError();
    }
}
