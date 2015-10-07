using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public partial interface IPlayerRepository
    {
        IQueryable<Player> GetForTeam(int teamId);
    }
}
