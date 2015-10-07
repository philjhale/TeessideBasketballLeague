using System.Collections.Generic;
using System.Linq;
using Basketball.Common.BaseTypes;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public partial class TeamService : BaseService<Team>, ITeamService
    {
        readonly ICompetitionService competitionService;

        public TeamService(ITeamRepository teamRepository,
            ICompetitionService competitionService)
            : base(teamRepository)
        {
            this.teamRepository = teamRepository;
            this.competitionService = competitionService;
        }

        public List<Team> GetTeamsForCurrentSeason()
        {
            var teams =  GetTeamsForSeason(competitionService.GetTeamLeaguesForCurrentSeason());

            return teams;
        }

        public List<Team> GetTeamsForSeason(List<TeamLeague> teamLeagueList)
        {
            List<Team> teamList = new List<Team>();

            foreach (TeamLeague tl in teamLeagueList)
                teamList.Add(tl.Team);

            return teamList;
        }

        public List<Team> GetTeamsForLeague(int leagueId)
        {
            return teamRepository.GetTeamsForLeague(leagueId).ToList();
        }
    }
}
