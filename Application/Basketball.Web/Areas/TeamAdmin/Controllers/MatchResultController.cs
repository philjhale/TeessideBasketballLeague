using System.Transactions;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using System.Collections.Generic;
using Basketball.Service.Exceptions;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.TeamAdmin.Controllers
{
    [TeamAdmin]
    public class MatchResultController : BaseController
    {
        private readonly IFixtureService fixtureService;
        private readonly IMatchResultService matchResultService;
        private readonly IPlayerService playerService;
        private readonly IMembershipService membershipService;
        private readonly IStatsReportingService statsReportingService;

        public MatchResultController(IFixtureService fixtureService,
            ICompetitionService competitionService,
            IMatchResultService matchResultService,
            IPlayerService playerService,
            IMembershipService membershipService,
            IStatsReportingService statsReportingService)
        {
            Check.Require(fixtureService != null, "fixtureService may not be null");
            Check.Require(competitionService != null, "competitionService may not be null");
            Check.Require(matchResultService != null, "statsService may not be null");
            Check.Require(playerService != null, "playerService may not be null");
            Check.Require(membershipService != null, "membershipService may not be null");
            Check.Require(statsReportingService != null, "statsService may not be null");

            this.fixtureService     = fixtureService;
            this.matchResultService = matchResultService;
            this.playerService      = playerService;
            this.membershipService  = membershipService;
            this.statsReportingService       = statsReportingService;
        }

        #region Actions

        public ActionResult Index()
        {
            List<Fixture> fixturesForTeam = new List<Fixture>();
            User currentUser = membershipService.GetUserByUserName(User.Identity.Name);
            
            // Get home fixtures for the team of the currently logged in user (current season only)
            if(currentUser.Team != null)
                fixturesForTeam = fixtureService.GetHomeTeamFixturesForCurrentSeason(currentUser.Team.Id);

            return View(fixturesForTeam);
        }

        public ActionResult Edit(int id)
        {
            Fixture fixture = null;
            User loggedInUser = membershipService.GetLoggedInUser();

            // I don't particularly like this level of role checking, but I want to reuse this controller
            // for both roles so it'll do
            if (loggedInUser.SiteAdmin)
                fixture = fixtureService.Get(id);
            else
                fixture = fixtureService.Get(id, loggedInUser.Team.Id);

            if (fixture == null)
                return RedirectToAction("Index");

            MatchResultViewModel model = new MatchResultViewModel();

            model.MapToModel(fixture);

            model.HomePlayerStats = model.MapToPlayerFixtureStats(this.statsReportingService.GetPlayerStatsForFixture(fixture.Id, fixture.HomeTeamLeague.Id), playerService.GetForTeam(fixture.HomeTeamLeague.Team.Id), fixture.HomeTeamLeague, fixture);
            model.AwayPlayerStats = model.MapToPlayerFixtureStats(this.statsReportingService.GetPlayerStatsForFixture(fixture.Id, fixture.AwayTeamLeague.Id), playerService.GetForTeam(fixture.AwayTeamLeague.Team.Id), fixture.AwayTeamLeague, fixture);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(MatchResultViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.MapMvps();

                    // Should validation methods go into the view model? 
                    model.ValidateFixture();
                    model.ValidatePlayerStats();

                    // I'm not particularly happy using transactions within a controller but at present,
                    // because saving a match result seems to require the use of several services rather
                    // than one, I can't think of a better way to do it
                    // EFCachingProvider doesn't support TransactionScope out of the box so changed source with the following https://gist.github.com/797390
                    using(TransactionScope scope = new TransactionScope())
                    {
                        Fixture fixtureToUpdate = fixtureService.Get(model.FixtureId);
                        fixtureToUpdate = model.MapToFixture(fixtureToUpdate);

                        // Save fixtures
                        matchResultService.SaveMatchResult(fixtureToUpdate, membershipService.GetLoggedInUser(), model.ForfeitingTeamId);

                        // Save all player stats including fixture, season, league and career stats
                        matchResultService.SaveMatchStats(model.HasPlayerStats, model.HomePlayerStats, fixtureToUpdate.HomeTeamLeague, fixtureToUpdate);
                        matchResultService.SaveMatchStats(model.HasPlayerStats, model.AwayPlayerStats, fixtureToUpdate.AwayTeamLeague, fixtureToUpdate);

                        scope.Complete();
                    }

                    SuccessMessage(FormMessages.SaveSuccess);

                    // I really dislike doing this, but it's an easy win so I'll turn a blind eye for now
                    if (membershipService.IsSiteAdmin(membershipService.GetLoggedInUserName()))
                        return RedirectToAction("Index", "Fixtures", new { area = "Admin" });

                    return RedirectToAction("Index");
                }
                catch (MatchResultScoresSameException)
                {
                    ErrorMessage(FormMessages.MatchResultScoresSame);
                }
                catch (MatchResultZeroTeamScoreException)
                {
                    ErrorMessage(FormMessages.MatchResultZeroTeamScore);
                }
                catch (MatchResultMaxPlayersExceededException)
                {
                    ErrorMessage(FormMessages.MatchResultMaxPlayersExceeded);
                }
                catch (MatchResultLessThanFivePlayersEachTeamException)
                {
                    ErrorMessage(FormMessages.MatchResultFivePlayersEachTeam);
                }
                catch (MatchResultSumOfScoresDoesNotMatchTotalException)
                {
                    ErrorMessage(FormMessages.MatchResultSumScoreDoesNotMatch);
                }
                catch (MatchResultNoMvpException)
                {
                    ErrorMessage(FormMessages.MatchResultNoMvp);
                }
                catch (MatchResultNoStatsMoreThanZeroPlayersException)
                {
                    ErrorMessage(FormMessages.MatchResultNoStatsZeroPlayers);
                }
            }

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

        //public ActionResult Forfeit(int? fixtureId, int? forfeitingTeamId)
        //{
        //    if(!fixtureId.HasValue || !forfeitingTeamId.HasValue)
        //        return RedirectToAction("Index");

        //    using(TransactionScope scope = new TransactionScope())
        //    {
        //        // TODO
        //        Fixture fixtureToUpdate = fixtureService.Get(fixtureId.Value);

        //        // Save fixture
        //        fixtureService.SaveForfeitedMatchResult(fixtureToUpdate, forfeitingTeamId.Value, membershipService.GetLoggedInUser());

        //        // Save all player stats including fixture, season, league and career stats
        //        statsService.SaveMatchStats(false, new List<PlayerFixtureStats>(), fixtureToUpdate.HomeTeamLeague, fixtureToUpdate);
        //        statsService.SaveMatchStats(false, new List<PlayerFixtureStats>(), fixtureToUpdate.AwayTeamLeague, fixtureToUpdate);

        //        scope.Complete();
        //    }

        //    SuccessMessage(FormMessages.SaveSuccess);
              
        //    return RedirectToAction("Index");
        //}

        #endregion

    }
}
