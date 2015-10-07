using System.Collections.Generic;
using System.Linq;
using Basketball.Common.BaseTypes;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public partial class PlayerService : BaseService<Player>, IPlayerService
    {
    
        public List<Player> GetForTeam(int teamId)
        {
            return playerRepository.GetForTeam(teamId).ToList();
        }

        /// <summary>
        /// Same as usual get but ensures the player exists on the specified team
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public Player Get(int playerId, int teamId)
        {
            return (from p in playerRepository.Get()
                        where p.Id == playerId && p.Team.Id == teamId
                        select p).SingleOrDefault();
        }
    }
}
