using System.Collections.Generic;
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
using Basketball.Web.Areas.Admin.Extensions;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class PlayersController : BaseController
    {
        private readonly IPlayerService playerService;
        private readonly ITeamService teamService;

        public PlayersController(IPlayerService playerService,
            ITeamService teamService)
        {
            Check.Require(playerService != null, "playerService may not be null");
            Check.Require(teamService != null, "teamService may not be null");

            this.playerService = playerService;
            this.teamService = teamService;
        }

        #region Actions
        public ActionResult Index()
        {
            return Index(null);
        }

        [HttpPost]
        public ActionResult Index(PlayersViewModel model)
        {
            if(model != null)
                Session.SetPlayerTeamIdFilter(model.FilterByTeamId);
            
            List<Player> players;

            if(Session.GetPlayerTeamIdFilter() > 0)
            {
                int teamIdFilter = Session.GetPlayerTeamIdFilter(); // Causes a linq to entities error if get value from session in expression below
                players = playerService.Get(x => x.Team.Id == teamIdFilter, orderBy: q => q.OrderBy(p => p.Forename).OrderBy(p => p.Surname));
            }
            else
                players = playerService.Get(orderBy: q => q.OrderBy(p => p.Forename).OrderBy(p => p.Surname));

            model = new PlayersViewModel(players, teamService.Get(), Session.GetPlayerTeamIdFilter());

            return View(model);
        }

        public ActionResult Create()
        {
            PlayerViewModel model = new PlayerViewModel();
            
            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PlayerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.TeamId.HasValue)
                    model.Player.Team = teamService.Get(model.TeamId.Value);
                
                playerService.Insert(model.Player);
                playerService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            
            PopulateStaticData(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Player player = playerService.Get(id);
            PlayerViewModel model = new PlayerViewModel();
            model.MapToModel(player);

            PopulateStaticData(model);
            
            return View(model);
        }

        // Don't know why but this causes an error
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PlayerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Player playerToUpdate = playerService.Get(model.Player.Id);

                TryUpdateModel(playerToUpdate, "Player");

                playerToUpdate.Team.Touch();
                playerToUpdate.Team = model.TeamId.HasValue ? this.teamService.Get(model.TeamId.Value) : null;

                playerService.Update(playerToUpdate);
                playerService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }

            PopulateStaticData(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            playerService.Delete(id);
            playerService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromTeam(int playerId)
        {
            Player player = playerService.Get(f => f.Id == playerId, includeProperties:"Team")[0];

            if(player != null)
            {
                player.Team = null;
                playerService.Update(player);
                playerService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);    
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region AJAX
        [HttpGet]
        public JsonResult GetPlayers()
        {
            return Request.IsAjaxRequest() ? this.Json(playerService.Get().Select(p => new { Name = p.ToString(), Id = p.Id, Team = (p.Team != null ? p.Team.TeamName : "None")}), JsonRequestBehavior.AllowGet) : null;
            //return Request.IsAjaxRequest() ? this.Json(playerService.Get().Select(p => p.ToString()), JsonRequestBehavior.AllowGet) : null;
        }

        #endregion

        private void PopulateStaticData(PlayerViewModel model)
        {
            model.Teams = teamService.GetTeamsForCurrentSeason().ToSelectListWithHeader(x => x.ToString(), x => x.Id.ToString(), model.TeamId.ToString(), "None");
        }
    }
}
