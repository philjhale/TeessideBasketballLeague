using Basketball.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Data
{
    public partial class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public IQueryable<Player> GetForTeam(int teamId)
        {
            return (from p in Get()
                    where p.Team.Id == teamId
                    orderby p.Forename, p.Surname
                    select p);
            //IList<Player> players = Session.CreateCriteria(typeof(Player))
            //    .Add(Expression.Eq("Team.Id", teamId))
            //    .AddOrder(Order.Asc("Forename"))
            //    .List<Player>();

            ////var query = from result in Session.Linq<Player>()
            //            //where result.Team.Id == teamId
            //            //orderby result.Forename
            //            //    select result;
   
            //return players;
        }

    }
}
