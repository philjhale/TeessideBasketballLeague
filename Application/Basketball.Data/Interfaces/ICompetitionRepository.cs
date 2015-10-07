using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public interface ICompetitionRepository
    {
        void Commit();

        Season GetCurrentSeason();
        IQueryable<Season> GetSeasons(Expression<Func<Season, bool>> filter = null, Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null);
        
        IQueryable<League> GetLeaguesForSeason(int seasonId);

        IQueryable<TeamLeague> GetTeamLeaguesForCurrentSeason();
        IQueryable<TeamLeague> GetTeamLeaguesForSeason(int seasonId);
        TeamLeague GetTeamLeagueByTeamIdInCurrentSeason(int teamId);
        TeamLeague GetTeamLeagueByTeamIdAndLeagueId(int teamId, int leagueId);
        IQueryable<TeamLeague> GetBasicStandingsForLeague(int leagueId);

        TeamLeague GetTeamLeague(int id);
        League GetLeague(int id);
        void InsertSeason(Season season);
        void UpdateTeamLeague(TeamLeague teamLeague);

        List<Fixture> GetFixturesForTeamLeagues(List<int> teamLeagueIds);
    }
}