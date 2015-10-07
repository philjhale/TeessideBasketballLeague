using Basketball.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Data
{
    public partial class PenaltyRepository : BaseRepository<Penalty>, IPenaltyRepository
    {
        public bool DoesPenaltyExist(int fixtureId, int teamId)
        {
            return (from p in Get()
                    where p.Team.Id == teamId
                     && p.Fixture.Id == fixtureId
                    select p).Any();
            //int count = Session.CreateCriteria(typeof(Penalty))
            //    .CreateAlias("Team", "t")
            //    .CreateAlias("Fixture", "f")
            //    .Add(Restrictions.Eq("t.Id", teamId))
            //    .Add(Restrictions.Eq("f.Id", fixtureId))
            //    .SetProjection(Projections.Count("Id"))
            //    .UniqueResult<int>();

            //return count > 0;
        }

        public IQueryable<Penalty> GetByTeamAndLeagueId(int teamId, int leagueId)
        {
            return (from p in Get()
                    where p.League.Id == leagueId
                        && p.Team.Id == teamId
                    select p);
        }
    }
}
