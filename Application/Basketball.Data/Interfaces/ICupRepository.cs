using System.Collections.Generic;
using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public partial interface ICupRepository
    {
        IQueryable<Cup> GetCupsForLeagues(List<League> leagues) ;

        IQueryable<Fixture> GetCupFixturesForSeason(int cupId, int seasonId);
    }
}
