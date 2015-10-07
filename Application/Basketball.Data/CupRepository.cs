using System.Collections.Generic;
using System.Linq;
using Basketball.Common.BaseTypes;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;

namespace Basketball.Data
{
    public partial class CupRepository : BaseRepository<Cup>, ICupRepository
    {
        private readonly ICupLeagueRepository cupLeagueRepository;
        private readonly IFixtureRepository fixtureRepository;

        public CupRepository(IDbContext context, ICupLeagueRepository cupLeagueRepository, IFixtureRepository fixtureRepository)
            : base(context)
        {
            this.cupLeagueRepository = cupLeagueRepository;
            this.fixtureRepository   = fixtureRepository;
        }

        public IQueryable<Cup> GetCupsForLeagues(List<League> leagues)
        {
            List<int> leagueIds = leagues.Select(l => l.Id).ToList();

            return from cl in cupLeagueRepository.Get() 
                   where leagueIds.Contains(cl.League.Id)
                   select cl.Cup;
        }

        public IQueryable<Fixture> GetCupFixturesForSeason(int cupId, int seasonId)
        {
            return from f in fixtureRepository.Get()
                   where f.IsCupFixture && f.Cup.Id == cupId
                        && f.HomeTeamLeague.League.Season.Id == seasonId
                    select f;
        }
    }
}
