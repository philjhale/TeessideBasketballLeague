using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Service.Interfaces
{
    public partial interface ICupService
    {
        List<Cup> GetCupsForCurrentSeason();

        List<Fixture> GetCupFixturesForCurrentSeason(int cupId);

        List<List<Fixture>> GetCupFixturesForDisplay(int cupId);

        List<List<Fixture>> GetCupFixturesByRoundForCurrentSeason(int cupId);

        List<List<Fixture>> OrderCupFixturesForDisplay(List<List<Fixture>> fixturesByRound);
    }
}
