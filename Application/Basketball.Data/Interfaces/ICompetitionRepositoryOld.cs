using System;
using Basketball.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Basketball.Data.Interfaces
{
    public interface ICompetitionRepository
    {
        TeamLeague GetByTeamIdAndLeagueId(int teamId, int leagueId);
        Season GetCurrentSeason();
        List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, string isCup);
        List<Fixture> GetFixturesWithLateResult(int seasonId, int maxHours);
        List<Fixture> GetLatestMatchResults(int numResults);
        List<League> GetLeaguesForSeason(int seasonId);
        List<TeamLeague> GetStandingsForLeague(int leagueId);
        List<Fixture> GetPlayedFixturesForTeamInReverseDateOrder(int teamLeagueId);
        List<Fixture> GetTeamHomeFixturesForCurrentSeason(int teamId);
        TeamLeague GetTeamLeague(int id);
        TeamLeague GetTeamLeagueInCurrentSeason(int teamId);
        List<TeamLeague> GetTeamLeaguesForCurrentSeason();
        Fixture GetFixture(int id);
        List<Team> GetTeamsForCurrentSeason();
        List<Season> GetSeasons(Expression<Func<Season, bool>> filter = null, Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null);
        void UpdateFixture(Fixture fixture);
        void Commit();
    }
}
