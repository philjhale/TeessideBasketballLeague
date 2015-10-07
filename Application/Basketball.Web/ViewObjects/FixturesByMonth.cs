using System;
using System.Collections.Generic;
using System.Globalization;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewObjects
{
    public class FixturesByMonth
    {
        public string           Month    { get; set; }
        public List<Fixture>    Fixtures { get; set; }

        public FixturesByMonth(DateTime currentDate, List<Fixture> fixtures)
        {
            Month         = string.Format("{0} {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentDate.Month), currentDate.Year);
            this.Fixtures = fixtures;
        }
    }
}