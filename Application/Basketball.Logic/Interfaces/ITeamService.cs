using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Service.Interfaces
{
    public partial interface ITeamService
    {
        List<Team> GetTeamsForCurrentSeason();
        List<Team> GetTeamsForLeague(int leagueId);
        List<Team> GetTeamsForSeason(List<TeamLeague> teamLeagueList);
    }
}