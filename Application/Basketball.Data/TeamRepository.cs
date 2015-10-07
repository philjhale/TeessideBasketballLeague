using System.Linq;
using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes;

namespace Basketball.Data
{
    public partial class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        // Don't like doing this but can't get teams for league without it
        readonly ITeamLeagueRepository teamLeagueRepository;

        public TeamRepository(IDbContext context, ITeamLeagueRepository teamLeagueRepository) : base(context)
        {
            this.teamLeagueRepository = teamLeagueRepository;
        }

        public IQueryable<Team> GetTeamsForLeague(int leagueId)
        {
            return (from t in teamLeagueRepository.Get()
                    where t.League.Id == leagueId
                    orderby t.TeamName
                    select t.Team);
        }

    }
}
