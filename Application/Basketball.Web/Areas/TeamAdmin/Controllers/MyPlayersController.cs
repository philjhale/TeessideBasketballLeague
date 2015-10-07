using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Common.BaseTypes;
using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.Areas.TeamAdmin.ViewModels;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.TeamAdmin.Controllers
{
    [TeamAdmin]
    public class MyPlayersController : BaseController
    {
        private readonly IPlayerService playerService;
        private readonly IMembershipService membershipService;


        public MyPlayersController(IPlayerService playerService,
            IMembershipService membershipService)
        {
            this.playerService = playerService;
            this.membershipService = membershipService;
        }

        #region Actions
        public ActionResult Index()
        {
            User user = membershipService.GetLoggedInUser();

            return View(playerService.GetForTeam(user.Team.Id));
        }

        public ActionResult Edit(int id)
        {
            Player playerToEdit = playerService.Get(id, membershipService.GetLoggedInUser().Team.Id);

            if (playerToEdit == null)
                return RedirectToAction("Index");
            
            EditPlayerViewModel model = new EditPlayerViewModel()
            {
                Player = playerToEdit
            };

            PopulateHeightLists(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditPlayerViewModel model)
        {
            if (ModelState.IsValid)
            {
                playerService.Save(model.Player);
                playerService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
            }

            PopulateHeightLists(model);

            return View(model);
        } 

        public ActionResult Remove(int id)
        {
            Player player = playerService.Get(f => f.Id == id, includeProperties:"Team")[0];

            // TODO Should probably check that the player id is in the team of the 
            // currently logged in user
            if(player != null)
            {
                player.Team = null;
                playerService.Update(player);
                playerService.Commit();

                SuccessMessage(FormMessages.MyPlayersPlayerRemoved);    
            }

            return RedirectToAction("Index");
        }
        #endregion


        private void PopulateHeightLists(EditPlayerViewModel model)
        {
            List<SelectListItem> heightList = new List<SelectListItem>();

            // Feet
            for (int i = 4; i <= 7; i++)
                heightList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });

            model.HeightFeet = heightList.ToSelectListWithHeader(x => x.Text,
                x => x.Value.ToString(),
                model.HeightFeet != null ? model.HeightFeet.ToString() : "");

            // Inches
            heightList.Clear();
            for (int i = 1; i <= 11; i++)
                heightList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });

            model.HeightInches = heightList.ToSelectListWithHeader(
                x => x.Text,
                x => x.Value.ToString(),
                model.HeightInches != null ? model.HeightInches.ToString() : "");
        }
    }
}
