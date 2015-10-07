using System;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Stores days of week. This is purely reference data.
    /// In fact there's probably not much point in this game being in the database
    /// </summary>
    public class DayOfWeek : Entity
    {
        public DayOfWeek() {}

        public DayOfWeek(string day)
        {
            this.Day = day;
        }

        public virtual string Day { get; set; }
    }
}
