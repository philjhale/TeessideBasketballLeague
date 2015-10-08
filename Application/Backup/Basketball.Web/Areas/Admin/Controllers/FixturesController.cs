using System.Linq;
using System.Web.Mvc;
using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Exceptions;
using Basketball.Web.Areas.Admin.ViewModels;
using Basketball.Web.Areas.TeamAdmin.ViewModels;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;
using Basketball.Web.ViewModels;
using Basketball.Service.Interfaces;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [FixtureAdmin]
    public class FixturesController : BaseController
    {
        private readonly ICompetitionService competitionService;
        private readonly IFixtureService fixtureService;
        private readonly ITeamService teamService;
        private readonly IRefereeService refereeService;
        private readonly IMembershipService membershipService;
        private readonly ICupService cupService;
        private readonly IOneOffVenueService oneOffVenueService;

        public FixturesController(ICompetitionService competitionService,
            IFixtureService fixtureService,
            ITeamService teamService,
            IRefereeService refereeService,
            IMembershipService membershipService,
            ICupService cupService,
            IOneOffVenueService oneOffVenueService)
        {
            this.competitionService = competitionService;
            this.fixtureService     = fixtureService;
            this.teamService        = teamService;
            this.refereeService     = refereeService;
            this.membershipService  = membershipService;
            this.cupService         = cupService;
            this.oneOffVenueService    = oneOffVenueService;
        }

        #region Actions
        public ActionResult Index()
        {
            FixturesViewModel model = new FixturesViewModel(
                competitionService.GetCurrentSeason().ToString(),
                teamService.GetTeamsForCurrentSeason(),
                competitionService.GetLeaguesForCurrentSeason(),
                cupService.GetCupsForCurrentSeason(),
                fixtureService.GetFixturesForCurrentSeasonFilter(-1, null, null)
            );

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FixturesViewModel model)
        {
            model.PopulateData(
                competitionService.GetCurrentSeason().ToString(),
                teamService.GetTeamsForCurrentSeason(),
                competitionService.GetLeaguesForCurrentSeason(),
                cupService.GetCupsForCurrentSeason(),
                fixtureService.GetFixturesForCurrentSeasonFilter(model.FilterByTeamId, model.FilterByIsPlayed, model.IsFilteredByLeague(), model.IsFilteredByCup(), model.GetLeagueOrCupId())
            );

            return View(model);
        }

        public ActionResult Cancel(int id)
        {
            Fixture fixtureToUpdate = fixtureService.Get(id);
            fixtureToUpdate.IsCancelled = "Y";
            fixtureService.Update(fixtureToUpdate);
            fixtureService.Commit();

            SuccessMessage(FormMessages.FixtureCancelled);
            return RedirectToAction("Index");
        }

        public ActionResult SelectLeague()
        {
            LeagueSelectViewModel model = new LeagueSelectViewModel()
            {
                Leagues = competitionService.GetLeaguesForCurrentSeason().ToSelectListWithHeader(x => x.ToString(), x => x.Id.ToString(), null)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectLeague(LeagueSelectViewModel selectedLeague)
        {
            //FixtureViewModel model = new FixtureViewModel();
            //model.LeagueId = selectedLeague.LeagueId;
            
            //PopulateStaticData(model);

            return RedirectToAction("Create", new { leagueOrCupId = selectedLeague.LeagueId, isCup = false });
        }

        public ActionResult SelectCup()
        {
            var model = new CupSelectViewModel()
            {
                Cups = cupService.Get().ToSelectListWithHeader(x => x.ToString(), x => x.Id.ToString(), null)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectCup(CupSelectViewModel selectedCup)
        {
            //FixtureViewModel model = new FixtureViewModel();
            //model.CupId = selectedLeague.CupId;
            
            //PopulateStaticData(model);

            return RedirectToAction("Create", new { leagueOrCupId = selectedCup.CupId, isCup = true });
        }

        public ActionResult Create(int leagueOrCupId, bool isCup)
        {
            FixtureViewModel model = new FixtureViewModel();
            model.LeagueOrCupId = leagueOrCupId;

            model.Fixture.IsCupFixture = isCup;

            PopulateStaticData(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FixtureViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Validate();

                    // TODO Ideally a service should do this
                    model.Fixture = model.MapToFixture(
                        model.Fixture,
                        this.competitionService.GetTeamLeagueByTeamIdInCurrentSeason(model.HomeTeamId),
                        this.competitionService.GetTeamLeagueByTeamIdInCurrentSeason(model.AwayTeamId), 
                        model.Referee1Id.HasValue ? this.refereeService.Get(model.Referee1Id.Value) : null, 
                        model.Referee2Id.HasValue ? this.refereeService.Get(model.Referee2Id.Value) : null, 
                        this.membershipService.GetLoggedInUser(),
                        model.OneOffVenueId.HasValue ? oneOffVenueService.Get(model.OneOffVenueId.Value) : null);
             
                    if(model.Fixture.IsCupFixture)
                        model.Fixture.Cup = cupService.Get(model.LeagueOrCupId);

                    fixtureService.Save(model.Fixture);
                    fixtureService.Commit();

                    SuccessMessage(FormMessages.SaveSuccess);

                    if (model.CreateAnotherFixture)
                        return RedirectToAction("Create", new { leagueOrCupId = model.LeagueOrCupId, isCup = model.Fixture.IsCupFixture });
                    else
                        return RedirectToAction("Index");
                }
            }
            catch (FixtureTeamsTheSameException)
            {
                ErrorMessage(FormMessages.FixtureTeamsTheSame);
            }

            PopulateStaticData(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            FixtureViewModel model = new FixtureViewModel(fixtureService.Get(id));
            model.MapToModel(model.Fixture);

            PopulateStaticData(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FixtureViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Validate();

                    Fixture fixtureToUpdate = fixtureService.Get(model.Fixture.Id);
                    TryUpdateModel(fixtureToUpdate, "Fixture");
                    
                    fixtureToUpdate = model.MapToFixture(
                        fixtureToUpdate,
                        this.competitionService.GetTeamLeagueByTeamIdInCurrentSeason(model.HomeTeamId),
                        this.competitionService.GetTeamLeagueByTeamIdInCurrentSeason(model.AwayTeamId), 
                        model.Referee1Id.HasValue ? this.refereeService.Get(model.Referee1Id.Value) : null, 
                        model.Referee2Id.HasValue ? this.refereeService.Get(model.Referee2Id.Value) : null, 
                        this.membershipService.GetLoggedInUser(),
                        model.OneOffVenueId.HasValue ? oneOffVenueService.Get(model.OneOffVenueId.Value) : null);

                    fixtureService.Save(fixtureToUpdate);
                    fixtureService.Commit();

                    SuccessMessage(FormMessages.SaveSuccess);

                    return RedirectToAction("Index");
                }
            }
            // TODO Create some kind of parent validation exception containing error property
            catch (FixtureTeamsTheSameException)
            {
                ErrorMessage(FormMessages.FixtureTeamsTheSame);
            }
            catch (FixtureRefereesTheSameException)
            {
                ErrorMessage(FormMessages.FixtureRefereesTheSame);
            }

            PopulateStaticData(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            fixtureService.Delete(id);
            fixtureService.Commit();

            SuccessMessage(FormMessages.DeleteSuccess);

            return RedirectToAction("Index");
        }

        #endregion


        private void PopulateStaticData(FixtureViewModel model)
        {
            // TODO Perhaps get only the teams involved in the cup?
            if(model.Fixture.IsCupFixture)
                model.Teams = teamService.GetTeamsForCurrentSeason().ToSelectList(x => x.ToString(), x => x.Id.ToString(), null);
            else
                model.Teams = teamService.GetTeamsForLeague(model.LeagueOrCupId).ToSelectList(x => x.ToString(), x => x.Id.ToString(), null);

            if(model.Fixture.IsCupFixture)
                model.LeagueOrCupName = cupService.Get(model.LeagueOrCupId).ToString();
            else
                model.LeagueOrCupName = competitionService.GetLeague(model.LeagueOrCupId).ToString();

            model.Referees1 = refereeService.Get(orderBy: q => q.OrderBy(x => x.Forename)).ToSelectListWithHeader(x => x.ToString(), x => x.Id.ToString(), null, "None");
            model.Referees2 = refereeService.Get(orderBy: q => q.OrderBy(x => x.Forename)).ToSelectListWithHeader(x => x.ToString(), x => x.Id.ToString(), null, "None");

            model.OneOffVenues = oneOffVenueService.Get(orderBy: q => q.OrderBy(x => x.Venue)).ToSelectListWithHeader(x => x.ToString(), x => x.Id.ToString(), null, "None");
        }

       
    }
}
