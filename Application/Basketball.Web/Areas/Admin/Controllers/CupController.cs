using System.Web.Mvc;
using Basketball.Common.Resources;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class CupController : BaseController
    {
        private readonly ICupService cupService;

        public CupController(ICupService cupService)
        {
             Check.Require(cupService != null, "cupService may not be null");

             this.cupService = cupService;
        }

        #region Actions
        public ActionResult Index()
        {
            return View(cupService.Get());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cup cup)
        {
            if (ModelState.IsValid)
            {
                cupService.Insert(cup);
                cupService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(cup);
        }

        public ActionResult Edit(int id)
        {
            return View(cupService.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cup cup)
        {
            if (ModelState.IsValid)
            {
                cupService.Update(cup);
                cupService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(cup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            cupService.Delete(id);
            cupService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        }
        #endregion

        //private void populateLeagueList(Cup cup)
        //{
        //    var checkBoxListInfoList = new List<CheckBoxListInfo>();
        //    IList<League> leaguesForCupList = null;

        //    // Get the leagues for the current season that have linked to the cup
        //    if(cup != null)
        //        leaguesForCupList = this.cupService.GetLeaguesForCup(cup.Id);

        //    foreach (var league in seasonRepository.GetLeaguesForSeason(seasonRepository.GetCurrentSeason().Id))
        //    {
        //        checkBoxListInfoList.Add(new CheckBoxListInfo(
        //            league.Id.ToString(),
        //            league.ToString(),
        //            HasLeague(leaguesForCupList, league))
        //        );

        //    }

        //    ViewData["LeagueList"] = checkBoxListInfoList;
        //}

        ///// <summary>
        ///// Return true if the the parameter league is in the list leagueList
        ///// </summary>
        ///// <param name="leagueList"></param>
        ///// <param name="league"></param>
        ///// <returns></returns>
        //private bool HasLeague(IList<League> leagueList, League league)
        //{
        //    if (leagueList == null)
        //        return false;

        //    return HasLeague(leagueList, league.Id);
        //}

        ///// <summary>
        ///// Return true if the parameter league matches a cup type id in the list leagueList
        ///// </summary>
        ///// <param name="leagueList"></param>
        ///// <param name="league"></param>
        ///// <returns></returns>
        //private bool HasLeague(IList<League> leagueList, int leagueId)
        //{
        //    if (leagueList == null)
        //        return false;

        //    foreach (League league in leagueList)
        //    {
        //        if (leagueId == league.Id)
        //            return true;
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// List items
        ///// </summary>
        ///// <returns></returns>
        ////[Transaction]
        //public ActionResult Index()
        //{
        //    IList<Cup> cupList = this.cupService.GetAllCups();
        //    return View(cupList);
        //}

        ///// <summary>
        ///// Show create screen
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Create()
        //{
        //    populateLeagueList(null);
        //    return View();
        //}

        //[ValidateAntiForgeryToken]      // Helps avoid CSRF attacks
        //[AcceptVerbs(HttpVerbs.Post)]   // Limits the method to only accept post requests
        //public ActionResult Create(Cup cup, int[] leagueIds)
        //{
        //    if (cup.IsValid())
        //    {
        //        this.cupService.SaveOrUpdate(cup);

        //        // Save the cupLeagues for each league id submitted
        //        if (leagueIds != null)
        //        {
        //            foreach (int i in leagueIds)
        //            {
        //                this.cupService.SaveOrUpdateCupLeague(
        //                    new CupLeague(cup, seasonRepository.GetLeague(i))
        //                );
        //            }
        //        }

        //        TempData[FormMessages.MessageTypeSuccess] = FormMessages.SaveSuccess;
        //        return RedirectToAction("Index");
        //    }

        //    populateLeagueList(cup);
        //    MvcValidationAdapter.TransferValidationMessagesTo(ViewData.ModelState,
        //        cup.ValidationResults());
        //    return View(cup);
        //}

        ///// <summary>
        ///// The transaction on this action is optional, but recommended for performance reasons
        ///// </summary>
        ////[Transaction]
        //public ActionResult Edit(int id)
        //{
        //    Cup cup = this.cupService.Get(id);
        //    populateLeagueList(cup);
        //    return View(cup);
        //}

        ///// <summary>
        ///// Accepts the form submission to update an existing item. This uses 
        ///// <see cref="DefaultModelBinder" /> since we're going to try to update the persisted 
        ///// entity and ask it for its validation state rather than relying on the employee
        ///// from the form to report validation issues.  This is particularly important when verifying
        ///// if the updated persistent object is unique when compared to other entities in the DB.
        ///// </summary>
        //[ValidateAntiForgeryToken]
        //[Transaction]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(int id, Cup cup, int[] leagueIds)
        //{
        //    Cup cupToUpdate = this.cupService.Get(id);
        //    TransferFormValuesTo(cupToUpdate, cup);
            
        //    if (cupToUpdate.IsValid())
        //    {
        //        this.cupService.SaveOrUpdate(cupToUpdate);

        //        // TODO Move this code into the repository
        //        IList<League> leaguesForCupList = this.cupService.GetLeaguesForCup(cupToUpdate.Id);
        //        // Delete cup types which exist in the database but were not submitted
        //        foreach (League league in leaguesForCupList)
        //        {
        //            // If cupTypeIds is null then all of the cup types should be deleted
        //            if (leagueIds == null ||
        //                    Array.Exists<int>(leagueIds, new Predicate<int>(delegate(int i) { return i == league.Id; })) == false)
        //                this.cupService.DeleteCupLeague(this.cupService.GetCupLeague(cupToUpdate.Id, league.Id));
        //        }

        //        // Add cup types submitted that do not exist in the database
        //        if (leagueIds != null)
        //        {
        //            foreach (int leagueId in leagueIds)
        //            {
        //                if (HasLeague(leaguesForCupList, leagueId) == false)
        //                    this.cupService.SaveOrUpdateCupLeague(
        //                        new CupLeague(cupToUpdate, seasonRepository.GetLeague(leagueId))
        //                    );
        //            }
        //        }

        //        TempData[FormMessages.MessageTypeSuccess] = FormMessages.SaveSuccess;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        this.cupService.DbContext.RollbackTransaction();
        //        populateLeagueList(cupToUpdate);
        //        MvcValidationAdapter.TransferValidationMessagesTo(ViewData.ModelState,
        //            cupToUpdate.ValidationResults());
        //        return View(cupToUpdate);
        //    }
        //}

        ///// <summary>
        ///// Pass values from form into object pulled from the database
        ///// </summary>
        ///// <param name="cupToUpdate"></param>
        ///// <param name="cupFromForm"></param>
        //private void TransferFormValuesTo(Cup cupToUpdate, Cup cupFromForm)
        //{
        //    cupToUpdate.CupName = cupFromForm.CupName;
        //}

        ///// <summary>
        ///// As described at http://stephenwalther.com/blog/archive/2009/01/21/asp.net-mvc-tip-46-ndash-donrsquot-use-delete-links-because.aspx
        ///// there are a lot of arguments for doing a delete via a GET request.  This addresses that, accordingly.
        ///// </summary>
        //[ValidateAntiForgeryToken]
        //[Transaction]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Delete(int id)
        //{
        //    Cup cupToDelete = this.cupService.Get(id);
        //    bool success = true;

        //    if (cupToDelete != null)
        //    {
        //        // Delete CupTypeRels first
        //        this.cupService.DeleteLeaguesForCup(cupToDelete.Id);
        //        // Then the cup
        //        this.cupService.Delete(cupToDelete);

        //        try
        //        {
        //            this.cupService.DbContext.CommitChanges();
                    
        //        }
        //        catch
        //        {
        //            success = false;   
        //            this.cupService.DbContext.RollbackTransaction();
        //        }
        //    }

        //    if(success)
        //        TempData[FormMessages.MessageTypeSuccess] = FormMessages.DeleteSuccess;
        //    else
        //        TempData[FormMessages.MessageTypeFailure] = FormMessages.DeleteFail;

        //    return RedirectToAction("Index");
        //}
    }
}
