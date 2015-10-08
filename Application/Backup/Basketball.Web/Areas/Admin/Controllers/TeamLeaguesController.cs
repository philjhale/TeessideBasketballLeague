using System.Linq;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.Areas.Admin.ViewModels;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    // TODO Validation to ensure there are no duplications. Or only show teams in the drop down that are not in team leagues
    [SiteAdmin]
    public class TeamLeaguesController : BaseController
    {
        private readonly ITeamLeagueService teamLeagueService;
        private readonly ICompetitionService competitionService;
        private readonly ITeamService teamService;

        public TeamLeaguesController(ITeamLeagueService teamLeagueService,
            ICompetitionService competitionService, 
            ITeamService teamService)
        {
            Check.Require(teamLeagueService != null, "teamLeagueService may not be null");
            Check.Require(competitionService != null, "competitionService may not be null");
            Check.Require(teamService != null, "teamService may not be null");

            this.teamLeagueService = teamLeagueService;
            this.competitionService = competitionService;
            this.teamService = teamService;
        }

        #region Actions
        public ActionResult Index()
        {
            return View(competitionService.GetTeamLeaguesForCurrentSeason());
        }

        public ActionResult Create()
        {
            TeamLeagueViewModel model = new TeamLeagueViewModel();
            
            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(TeamLeagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                Team team = teamService.Get(model.TeamId);
                TeamLeague teamLeague = new TeamLeague(competitionService.GetLeague(model.LeagueId), team, team.TeamName, team.TeamNameLong);

                teamLeagueService.Insert(teamLeague);
                teamLeagueService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            
            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            teamLeagueService.Delete(id);
            teamLeagueService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        } 
        #endregion


        private void PopulateStaticData(TeamLeagueViewModel model)
        {
            model.Teams = teamService.Get(orderBy: q => q.OrderBy(t => t.TeamNameLong)).ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.TeamId.ToString());
            model.Leagues = competitionService.GetLeaguesForCurrentSeason().ToSelectList(x => x.Season.ToString() + " " + x.ToString(), x => x.Id.ToString(), model.LeagueId.ToString());
        }
    }
}
