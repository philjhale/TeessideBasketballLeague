using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public partial interface ITeamRepository
    {
        IQueryable<Team> GetTeamsForLeague(int leagueId);
    }
}