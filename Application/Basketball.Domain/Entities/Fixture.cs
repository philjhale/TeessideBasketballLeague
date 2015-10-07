using System;
using System.ComponentModel.DataAnnotations.Schema;

using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;
using Basketball.Common.Validation;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Stores fixture information include team and away team information,
    /// scores and match report
    /// </summary>
    public class Fixture : Entity
    {
        public Fixture() 
        {
            IsPlayed         = "N";
            IsCancelled      = "N";
            HasPlayerStats   = "Y";
            IsCupFixture     = false;
            IsPenaltyAllowed = true;
            this.LastUpdated = DateTime.Now;
        }


        public Fixture(TeamLeague homeTeamLeague, TeamLeague awayTeamLeague, DateTime fixtureDate, User lastUpdatedBy)
            : this()
        {
            // Check for required values
            Check.Require(homeTeamLeague != null, "homeTeamLeague must be provided");
            Check.Require(awayTeamLeague != null, "awayTeamLeague must be provided");
            Check.Require(lastUpdatedBy != null, "lastUpdatedBy must be provided");

            this.HomeTeamLeague = homeTeamLeague;
            this.AwayTeamLeague = awayTeamLeague;
            this.FixtureDate    = fixtureDate;
            this.LastUpdatedBy  = lastUpdatedBy;
        }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual TeamLeague HomeTeamLeague { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual TeamLeague AwayTeamLeague { get; set; }

        public virtual Referee Referee1 { get; set; }
        public virtual Referee Referee2 { get; set; }

        public int NumberOfRefs()
        {
            return (Referee1 != null ? 1 : 0)
                    +
                   (Referee2 != null ? 1 : 0);
        }

        private DateTime fixtureDate;
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual DateTime FixtureDate
        {
            get { return fixtureDate; } 
            set
            {
                if(value != fixtureDate)
                    IsCancelled = "N";
                fixtureDate = value;
            }
        } 

        [NotMapped]
        public virtual string Day
        {
            get { return FixtureDate.Day.AddDayOfMonthSuffix(); }
        }

        public virtual int? HomeTeamScore { get; set; }
        public virtual int? AwayTeamScore { get; set; }
        public virtual string Report { get; set; }
        public virtual bool IsCupFixture { get; set; }

        [Integer]
        public virtual int? CupRoundNo { get; set; }

        public virtual string CupRoundName { get; set; }

        public virtual Cup Cup { get; set; }
        public virtual string IsPlayed { get; set; } // TODO Boolean?
        public virtual DateTime? ResultAddedDate { get; set; }
        public virtual string IsCancelled { get; set; }

        /// <summary>
        /// This field was added to allow for one off tip times. Normally this would be 
        /// copied directly from Team
        /// </summary>
        [StringLength(5, ErrorMessage = FormMessages.FieldTooLong)]
        [RegularExpression(@"^$|^[0-2][0-9]:[0-5][0-9]$", ErrorMessage = FormMessages.FieldTwentyFourHour)]
        public virtual string TipOffTime { get; set; }

        public virtual string HasPlayerStats { get; set; }

        // This flag allows penalties to be prevent in certain valid situations. E.g. A game is cancelled,
        // then a walkover is claimed two months later
        public virtual bool IsPenaltyAllowed { get; set; }

        public virtual DateTime? LastUpdated { get; set; }
        public virtual User LastUpdatedBy { get; set; }

        public virtual bool IsForfeit { get; set; }
        public virtual Team ForfeitingTeam { get; set; }

        public virtual OneOffVenue OneOffVenue { get; set; }

        public bool HasOneOffVenue()
        {
            return OneOffVenue != null;
        }

        public string GetCupOrLeagueName()
        {
            return IsCupFixture ? Cup.ToString() : HomeTeamLeague.League.ToString();
        }

        #region Utility methods
        public bool IsHomeForfeit()
        {
            return IsPlayed == "Y" && IsForfeit && ForfeitingTeam.Id == HomeTeamLeague.Team.Id;
        }

        public bool IsAwayForfeit()
        {
            return IsPlayed == "Y" && IsForfeit && ForfeitingTeam.Id == AwayTeamLeague.Team.Id;
        }

        public bool IsHomeWin()
        {
            return IsPlayed == "Y" && (IsAwayForfeit() || HomeTeamScore > AwayTeamScore);
        }

        public bool IsAwayWin()
        {
            return IsPlayed == "Y" && (IsHomeForfeit() || AwayTeamScore > HomeTeamScore);
        }

        public bool IsHomeTeam(TeamLeague teamLeague)
        {
            if(teamLeague == null)
                throw new ArgumentException("Parameter teamLeague cannot be null");

            return this.HomeTeamLeague.Id == teamLeague.Id;
        }

        public bool IsAwayTeam(TeamLeague teamLeague)
        {
            if(teamLeague == null)
                throw new ArgumentException("Parameter teamLeague cannot be null");

            return this.AwayTeamLeague.Id == teamLeague.Id;
        }
        #endregion
    }
}
