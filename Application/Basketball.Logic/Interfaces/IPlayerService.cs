using System.Collections.Generic;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Domain.Entities;

namespace Basketball.Service.Interfaces
{
    public partial interface IPlayerService : IBaseService<Player>
    {
        List<Player> GetForTeam(int teamId);
        Player Get(int playerId, int teamId);
    }
}