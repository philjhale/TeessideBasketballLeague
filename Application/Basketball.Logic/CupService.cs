using System.Collections.Generic;
using System.Linq;
using Basketball.Common.BaseTypes;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public partial class CupService : BaseService<Cup>, ICupService
    {
        private readonly ICompetitionService competitionService;

        public CupService(ICupRepository cupRepository, ICompetitionService competitionService) 
            : base(cupRepository)
        {
            this.competitionService = competitionService;
            this.cupRepository      = cupRepository;
        }

        public List<Cup> GetCupsForCurrentSeason()
        {
            return cupRepository.GetCupsForLeagues(competitionService.GetLeaguesForCurrentSeason()).ToList();
        }

        public List<Fixture> GetCupFixturesForCurrentSeason(int cupId)
        {
            return cupRepository.GetCupFixturesForSeason(cupId, competitionService.GetCurrentSeason().Id).ToList();
        }

        public List<List<Fixture>> GetCupFixturesForDisplay(int cupId)
        {
            return OrderCupFixturesForDisplay(GetCupFixturesByRoundForCurrentSeason(cupId));
        }

        public List<List<Fixture>> GetCupFixturesByRoundForCurrentSeason(int cupId)
        {
            List<Fixture> cupFixtures = GetCupFixturesForCurrentSeason(cupId);
            List<List<Fixture>> fixturesByRound = new List<List<Fixture>>();

            // Group fixtures by round
            for (int i = 0; i < cupFixtures.Max(cf => cf.CupRoundNo); i++)
            {
                fixturesByRound.Add(cupFixtures.Where(cf => cf.CupRoundNo == i + 1).ToList());
            }

            return fixturesByRound;
        }

        /// <summary>
        /// Ensures fixtures are in the correct place for the cup diagram thingy
        /// </summary>
        /// <param name="fixturesByRound"></param>
        /// <returns></returns>
        public List<List<Fixture>> OrderCupFixturesForDisplay(List<List<Fixture>> fixturesByRound)
        {
            int numberOfRounds = fixturesByRound.Count;

            // Ignore the final round. If it's 2 or 4. We base the previous fixtures on the next round fixtures
            for (int i = numberOfRounds-1; i > 0; i--)
            {
                // We can ignore the final two rounds because there are only 2 and 1 fixtures played respectively
                // so teams will always be in the right place
                if(fixturesByRound[i].Count <= 2)
                    continue;

                // This works by looping through the fixtures in the next round an making sure the
                // fixtures in the current round are in the same order, if that makes any sense
                List<Fixture> fixturesForNextRound = fixturesByRound[i];
                List<Fixture> fixturesForCurrentRound = fixturesByRound[i-1];
                List<Fixture> reorderedFixturesForCurrentRound = new List<Fixture>();

                foreach (var fixtureInNextRound in fixturesForNextRound)
                {
                    reorderedFixturesForCurrentRound.Add(fixturesForCurrentRound.SingleOrDefault(f => f.HomeTeamLeague.Team.Id == fixtureInNextRound.HomeTeamLeague.Team.Id || f.AwayTeamLeague.Team.Id == fixtureInNextRound.HomeTeamLeague.Team.Id));
                    reorderedFixturesForCurrentRound.Add(fixturesForCurrentRound.SingleOrDefault(f => f.HomeTeamLeague.Team.Id == fixtureInNextRound.AwayTeamLeague.Team.Id || f.AwayTeamLeague.Team.Id == fixtureInNextRound.AwayTeamLeague.Team.Id));
                }

                fixturesByRound[i-1] = reorderedFixturesForCurrentRound;
            }

            // While fixtures in the last round is greater than 1 (i.e. The last round)
            while(fixturesByRound[fixturesByRound.Count-1].Count > 1)
            {
                List<Fixture> fixtures = new List<Fixture>();
                for (int i = 0; i < fixturesByRound[fixturesByRound.Count-1].Count / 2; i++)
                {
                    fixtures.Add(new FakeCupFixture(fixturesByRound.Count+1));
                }
                fixturesByRound.Add(fixtures);
            }

            // Fill any missing round fixtures will byes
            if(fixturesByRound.Count > 1)
            {
                if(fixturesByRound[0].Count < fixturesByRound[1].Count)
                {
                    int byesToAdd = (fixturesByRound[1].Count * 2) - fixturesByRound[0].Count;
                    for (int i = 0; i < byesToAdd; i++)
                    {
                        fixturesByRound[0].Add(new FirstRoundBye());
                    }
                }
            }

            // Not 100% sure about this. Need to allow for first round byes. This is the only way I can think of
            for (int i = 0; i < fixturesByRound[0].Count; i++)
            {
                if(fixturesByRound[0][i] == null)
                    fixturesByRound[0][i] = new FirstRoundBye(); 
            }

            return fixturesByRound;
        }
    }
}
