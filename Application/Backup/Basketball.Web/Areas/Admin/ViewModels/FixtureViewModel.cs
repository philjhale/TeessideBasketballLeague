using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Basketball.Common.Extensions;
using Basketball.Domain.Entities;
using Basketball.Service.Exceptions;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class FixtureViewModel
    {
        public Fixture Fixture { get; set; }
        
        public int LeagueOrCupId { get; set; }
        public string LeagueOrCupName { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public int? Referee1Id { get; set; }
        public int? Referee2Id { get; set; }

        public bool CreateAnotherFixture { get; set; }
        
        public List<SelectListItem> Teams { get; set; }

        // The need for duplicate referee lists is fucking bonkers
        // If only one exists and the first ref is set and the second isn't both the lists
        // will appear set because the list object will have it's selected value set and 
        // then it's reused for ref2
        public List<SelectListItem> Referees1 { get; set; }
        public List<SelectListItem> Referees2 { get; set; }

        public List<SelectListItem> CupRoundNames { get; private set; }

        public List<SelectListItem> OneOffVenues { get; set; }
        public int? OneOffVenueId { get; set; }

        public FixtureViewModel()
        {
            Fixture = new Fixture();
            Fixture.FixtureDate = DateTime.Now;

            SetCupRoundNames();
        }

        public FixtureViewModel(Fixture fixture)
        {
            Fixture = fixture;
            SetCupRoundNames();
        }

        public void MapToModel(Fixture fixture)
        {
            LeagueOrCupId         = fixture.IsCupFixture ? fixture.Cup.Id : fixture.HomeTeamLeague.League.Id;
            HomeTeamId            = fixture.HomeTeamLeague.Team.Id;
            AwayTeamId            = fixture.AwayTeamLeague.Team.Id;
            Referee1Id            = fixture.Referee1 != null ? fixture.Referee1.Id : (int?)null;
            Referee2Id            = fixture.Referee2 != null ? fixture.Referee2.Id : (int?)null;
            OneOffVenueId         = fixture.OneOffVenue != null ? fixture.OneOffVenue.Id : (int?)null;
        }

        public Fixture MapToFixture(Fixture fixture, TeamLeague homeTeamLeague, TeamLeague awayTeamLeague, Referee referee1, Referee referee2, User lastUpdatedBy, OneOffVenue oneOffVenue)
        {
            fixture.HomeTeamLeague = homeTeamLeague;
            fixture.AwayTeamLeague = awayTeamLeague;
            fixture.LastUpdatedBy  = lastUpdatedBy;
            fixture.LastUpdated    = DateTime.Now;

            // If you remove the few lines below accessing the referee objects you cannot reliably
            // set the properties to null. The assignment seems to be ignored.
            // By best guess is that the properties aren't lazy loaded until accessed so if you assign
            // a value to the property then read from it, the value gets loaded from the database. Fucked up.
            fixture.Referee1.Touch();
            fixture.Referee2.Touch();

            fixture.Referee1 = referee1;
            fixture.Referee2 = referee2;

            fixture.OneOffVenue.Touch();
            fixture.OneOffVenue = oneOffVenue;

            return fixture;
        }

        /// <exception cref="FixtureTeamsTheSameException"></exception>
        /// <exception cref="FixtureRefereesTheSameException"></exception>
        public void Validate()
        {
            if (HomeTeamId == AwayTeamId)
                throw new FixtureTeamsTheSameException();
            if(Referee1Id != null && Referee2Id != null
                && Referee1Id == Referee2Id)
                throw new FixtureRefereesTheSameException();
        }

        private void SetCupRoundNames()
        {
            CupRoundNames = new List<SelectListItem>();
            CupRoundNames.Add(new SelectListItem() { Text = "None", Value = "" });
            CupRoundNames.Add(new SelectListItem() { Text = "Quarter final", Value = "Quarter final" });
            CupRoundNames.Add(new SelectListItem() { Text = "Semi final", Value = "Semi final" });
            CupRoundNames.Add(new SelectListItem() { Text = "Final", Value = "Final" });
        }
    }
}