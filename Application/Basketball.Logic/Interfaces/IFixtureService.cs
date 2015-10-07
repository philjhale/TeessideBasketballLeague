using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Service.Interfaces
{
    public partial interface IFixtureService
    {
        List<Fixture> GetHomeTeamFixturesForCurrentSeason(int teamId);
        List<Fixture> GetLatestMatchResults(int? numMatchResults = null);
        List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, bool isLeague, bool isCup, int? leagueOrCupId);
        List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, bool? isCup);
        List<Fixture> GetFixturesForCurrentSeasonFilter();
        List<Fixture> GetAllFixturesForCurrentSeason();
        List<Fixture> GetPlayedFixturesForTeamInReverseDateOrder(int teamLeagueId);
        List<Fixture> GetFixturesThatCanBePenalised(int seasonId);
        Fixture Get(int fixtureId, int homeTeamId);
        int CountFixturesForTeam(int teamId);
        List<Fixture> GetHistoricFixturesBetweenTeams(int homeTeamId, int awayTeamId, int currentFixtureId);
    }
}