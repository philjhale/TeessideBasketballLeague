using System;
using System.Collections.Generic;
using Basketball.Domain;

namespace Basketball.Data.Interfaces
{
    public partial interface IFixtureRepository
    {
        List<Fixture> GetFixturesWithLateResult(int seasonId, int maxHours);
        List<Fixture> GetLatestMatchResults(int numResults);
    }
}
