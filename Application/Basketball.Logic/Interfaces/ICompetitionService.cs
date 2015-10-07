using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Basketball.Domain.Entities;

namespace Basketball.Service.Interfaces
{
    public interface ICompetitionService
    {
        void Commit();

        Season GetCurrentSeason();
        List<Season> GetSeasons(Expression<Func<Season, bool>> filter = null, Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null);
        List<Season> GetSeasons();
        Season CreateNextSeason(Season currentSeason);

        List<League> GetLeaguesForSeason(int seasonId);
        List<League> GetLeaguesForCurrentSeason();

        List<TeamLeague> GetStandingsForLeague(int leagueId);
        //TeamLeague UpdateTeamLeagueStats(int teamLeagueId);
        List<TeamLeague> GetTeamLeaguesForCurrentSeason();
        TeamLeague GetTeamLeagueByTeamIdInCurrentSeason(int teamId);

        // CRUD
        void UpdateTeamLeague(TeamLeague teamLeague);
        TeamLeague GetTeamLeagueByTeamIdAndLeagueId(int teamId, int leagueId);
        List<int> GetTeamLeagueIdsForCurrentSeason();
        TeamLeague GetTeamLeague(int id);
        League GetLeague(int id);
        void SaveSeason(Season season);
    }
}