using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class ScheduledTaskController : BaseController
    {
        private readonly IOptionService optionService;
        private readonly IFixtureService fixtureService;
        private readonly IPenaltyService penaltyService;
        private readonly ICompetitionService competitionService;
        private readonly IMatchResultService matchResultService;

        public ScheduledTaskController(IOptionService optionService,
            IFixtureService fixtureService,
            IPenaltyService penaltyService,
            ICompetitionService competitionService,
            IMatchResultService matchResultService)
        {
            Check.Require(optionService      != null, "optionService may not be null");
            Check.Require(fixtureService     != null, "fixtureService may not be null");
            Check.Require(penaltyService     != null, "penaltyService may not be null");
            Check.Require(competitionService != null, "competitionService may not be null");
            Check.Require(matchResultService != null, "statsService may not be null");

            this.optionService      = optionService;
            this.fixtureService     = fixtureService;
            this.penaltyService     = penaltyService;
            this.competitionService = competitionService;
            this.matchResultService = matchResultService;
        }

        /// <summary>
        /// Just returns a response. Only used for testing purposes
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult CheckLateMatchResults()
        {
            int penaltyPoints = 0;
            List<Fixture> lateResults = null;
            List<string> penaltyResults = new List<string>();
            string output;
            TeamLeague teamLeague = null;
      
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    penaltyPoints = Int32.Parse(optionService.GetByName(Option.SCHEDULE_LATE_RESULT_PEN_POINTS));

                    // Get fixtures for current season with late match results
                    lateResults = fixtureService.GetFixturesThatCanBePenalised(competitionService.GetCurrentSeason().Id);

                    // Loop through results and check whether they have penalties already
                    foreach (Fixture fixture in lateResults)
                    {
                        if (!penaltyService.DoesPenaltyExist(fixture.Id, fixture.HomeTeamLeague.Team.Id))
                        {
                            output = fixture.HomeTeamLeague.TeamNameLong + " penalised " + penaltyPoints + " point(s) for late match result vs "
                                + fixture.AwayTeamLeague.TeamNameLong + " on " + fixture.FixtureDate.ToShortDateString() + " *** AUTOMATIC INSERT";

                            // Penalty desn't exist, so insert one
                            penaltyService.Insert(
                                new Penalty(fixture.HomeTeamLeague.League,
                                    fixture.HomeTeamLeague.Team,
                                    penaltyPoints,
                                    output,
                                    fixture)
                            );

                            // Too many fecking commits here
                            penaltyService.Commit();

                            // Update team stats. Slight overkill, but there we go
                            teamLeague = penaltyService.UpdateTeamLeaguePenaltyPoints(fixture.HomeTeamLeague.League.Id, fixture.HomeTeamLeague.Team.Id);
                            penaltyService.Commit();

                            teamLeague = this.matchResultService.UpdateTeamLeagueStats(teamLeague.Id);
                            this.matchResultService.Commit();

                            competitionService.UpdateTeamLeague(teamLeague);
                            competitionService.Commit();

                            penaltyResults.Add(output);
                        }
                    }                    

                    scope.Complete();
                }
               

                ViewData["Results"] = penaltyResults;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Exception"] = ex;
            }

            return View();
        }

    }
}
