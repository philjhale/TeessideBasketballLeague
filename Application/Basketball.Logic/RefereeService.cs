using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Service
{
    public partial class RefereeService : BaseService<Referee>, IRefereeService
    {
        private readonly IFixtureService fixtureService;

        public RefereeService(IRefereeRepository refereeRepository,
            IFixtureService fixtureService)
            : base(refereeRepository)
        {
            this.refereeRepository = refereeRepository;
            this.fixtureService = fixtureService;
        }

        public List<RefereeWithCurrentSeasonFixtureCount> GetAllRefereesWithCurrentSeasonFixtureCount()
        {
            List<Referee> refs = refereeRepository.Get().ToList();
            List<Fixture> currentSeasonFixtures = fixtureService.GetAllFixturesForCurrentSeason();

            List<RefereeWithCurrentSeasonFixtureCount> refsWithFixtureCount = new List<RefereeWithCurrentSeasonFixtureCount>();

            foreach (Referee referee in refs)
            {
                RefereeWithCurrentSeasonFixtureCount refWithCount = new RefereeWithCurrentSeasonFixtureCount(referee, 
                    currentSeasonFixtures.Where(x => (x.Referee1 != null && x.Referee1.Id == referee.Id) || (x.Referee2 != null && x.Referee2.Id == referee.Id)).Count());

                refsWithFixtureCount.Add(refWithCount);
                
            }

            return refsWithFixtureCount.OrderBy(x => x.Referee.Forename).ToList();
        }
    }
}
