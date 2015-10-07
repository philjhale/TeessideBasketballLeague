using System;
using System.Linq;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Service.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Service
{
    public partial class PenaltyService : BaseService<Penalty>, IPenaltyService
    {
        readonly ITeamLeagueRepository teamLeagueRepository;
        //IPenaltyRepository penaltyRepository;

        public PenaltyService(IPenaltyRepository penaltyRepository,
            ITeamLeagueRepository teamLeagueRepository) : base(penaltyRepository)
        {
            this.penaltyRepository = penaltyRepository;
            this.teamLeagueRepository = teamLeagueRepository;
        }


        public TeamLeague UpdateTeamLeaguePenaltyPoints(int leagueId, int teamId)
        {
            TeamLeague teamLeague = null;

            IQueryable<Penalty> teamPenalty = penaltyRepository.GetByTeamAndLeagueId(teamId, leagueId);

            int points = 0;
            if(teamPenalty.Count() > 0)
                points = teamPenalty.Sum(p => p.Points);

            // Get the associated team league and update the penalty points
            // This is a bit dodge
            teamLeague = (teamLeagueRepository.Get(x => (x.League.Id == leagueId && x.Team.Id == teamId))).First();

            if (teamLeague != null)
                teamLeague.PointsPenalty = points;

            return teamLeague;
        }

        public bool DoesPenaltyExist(int fixtureId, int teamId)
        {
            return penaltyRepository.DoesPenaltyExist(fixtureId, teamId);
        }
    }
}
