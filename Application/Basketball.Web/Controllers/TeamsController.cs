using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Basketball.Domain.Entities;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using Basketball.Service.Interfaces;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.Controllers
{
    public class TeamsController : BaseController
    {
        readonly ICompetitionService competitionService;
        private readonly IStatsReportingService statsReportingService;
        private readonly ITeamService teamService;
		
		public TeamsController(ICompetitionService competitionService,
            IStatsReportingService playerService,
            ITeamService teamService)
        {
            this.competitionService = competitionService;
		    this.statsReportingService = playerService;
            this.teamService = teamService;
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult Index()
        {
            TeamsViewModel model = new TeamsViewModel();
            List<Team> teams = teamService.GetTeamsForCurrentSeason().OrderBy(x => x.TeamNameLong).ToList();
            string firstLetter = null;
            TeamsStartingWithLetter teamsStartingWithLetter = new TeamsStartingWithLetter();

            foreach (Team team in teams)
            {
                // Make firstLetter equal to the first letter of the team name
                // and start a new list of teams starting with the same letter
                if (firstLetter == null)
                {
                    firstLetter = team.ToString().ToUpper().Substring(0, 1);
                    teamsStartingWithLetter.Letter = firstLetter;
                    teamsStartingWithLetter.Teams.Add(team);
                }
                // If the first letter of the current team name is the same as the last, add
                // it too the same list
                else if (firstLetter.ToUpper() == team.ToString().ToUpper().Substring(0, 1))
                    teamsStartingWithLetter.Teams.Add(team);
                // If the first letter of the current team name is different from the last,
                // start a new list
                else if (firstLetter.ToUpper() != team.ToString().ToUpper().Substring(0, 1))
                {
                    model.TeamsStartingWithLetter.Add(teamsStartingWithLetter);
                    firstLetter = team.ToString().ToUpper().Substring(0, 1);
                    teamsStartingWithLetter = new TeamsStartingWithLetter();
                    teamsStartingWithLetter.Letter = firstLetter;
                    teamsStartingWithLetter.Teams.Add(team);
                }
            }

            model.TeamsStartingWithLetter.Add(teamsStartingWithLetter);

            return View(model);
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult View(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Team team = teamService.Get(id.Value);

            if (team == null)
                return RedirectToAction("Index", "Home");

            TeamViewModel model = new TeamViewModel()
            {
                Team                        = team,
                Players                     = statsReportingService.GetPlayerSeasonStatsForTeamAndSeason(id.Value, competitionService.GetCurrentSeason().Id),
                LeagueWins                  = statsReportingService.GetLeagueWinsForTeam(id.Value),
                CupWins                     = statsReportingService.GetCupWinsForTeam(id.Value),
                TotalWins                   = statsReportingService.GetTotalWins(id.Value),
                TotalLosses                 = statsReportingService.GetTotalLosses(id.Value),
                TotalPointsFor              = statsReportingService.GetTotalPointsFor(id.Value),
                TotalPointsAgainst          = statsReportingService.GetTotalPointsAgainst(id.Value),
                AveragePointsPerGameFor     = statsReportingService.GetAveragePointsPerGameForTeamFor(id.Value),
                AveragePointsPerGameAgainst = statsReportingService.GetAveragePointsPerGameForTeamAgainst(id.Value),
                BiggestHomeWin              = statsReportingService.GetBiggestHomeWinForTeam(id.Value),
                BiggestAwayWin              = statsReportingService.GetBiggestAwayWinForTeam(id.Value)
            };
            
            return View(model);
        }
    }
}
