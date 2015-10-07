using System.Collections.Generic;
using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public partial interface IFixtureRepository
    {
        IQueryable<Fixture> GetTeamHomeFixturesForTeamLeagues(int teamId, List<int> teamLeagueIds);

        IQueryable<Fixture> GetFixturesForTeamLeaguesFilter(int teamId, List<int> teamLeagueIds, string isPlayed, bool isLeague, bool isCup, int? leagueOrCupId);
        IQueryable<Fixture> GetFixturesForTeamLeaguesFilter(int teamId, List<int> teamLeagueIds, string isPlayed, bool? isCup);
        IQueryable<Fixture> GetFixturesForTeamLeagues(List<int> teamLeagueIds);
        IQueryable<Fixture> GetPlayedFixturesForTeamInReverseDateOrder(int teamLeagueId);
        IQueryable<Fixture> GetLatestMatchResults(int numResults);
        IQueryable<Fixture> GetFixturesThatCanBePenalised(int seasonId, int numberOfDays);
        IQueryable<Fixture> GetFixturesForTeam(int teamId);
        IQueryable<Fixture> GetHistoricFixturesBetweenTeams(int homeTeamId, int awayTeamId, int currentFixtureId);
    }
}