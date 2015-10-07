using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Domain.Entities.ValueObjects;

namespace Basketball.Service.Interfaces
{
    public partial interface IRefereeService : IBaseService<Referee>
    {
        List<RefereeWithCurrentSeasonFixtureCount> GetAllRefereesWithCurrentSeasonFixtureCount();
    }
}
