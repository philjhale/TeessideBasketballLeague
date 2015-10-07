using Basketball.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Data
{
    public partial class EventRepository : BaseRepository<Event>, IEventRepository
    {
        //public EventRepository(IDbContext context) : base(context) {}

        public IQueryable<Event> GetTopInFuture(int numResults)
        {
            //return Session.CreateCriteria(typeof(Event))
            //    .Add(Expression.Or(
            //        Expression.Ge("Date", DateTime.Today),
            //        Expression.Eq("Date", DateTime.Today)
            //    ))
            //    .SetMaxResults(numResults)
            //    .AddOrder(Order.Asc("Date"))
            //    .List<Event>();
            IQueryable<Event> q = (from r in Get() 
                             where r.Date > DateTime.Now 
                             orderby r.Date ascending 
                             select r)
                             .Take<Event>(numResults);

            return q;
        }
    }
}
