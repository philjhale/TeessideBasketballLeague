using System.Linq;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes.Interfaces;

namespace Basketball.Data.Interfaces
{
    public partial interface IPenaltyRepository : IBaseRepository<Penalty>
    {
        bool DoesPenaltyExist(int fixtureId, int teamId);
        IQueryable<Penalty> GetByTeamAndLeagueId(int teamId, int leagueId);
    }
}
