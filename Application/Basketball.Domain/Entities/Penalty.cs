using System;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    public class Penalty : Entity
    {
        public Penalty() 
        {
            PenaltyDate = DateTime.Now;
        }

        public Penalty(League league, Team team, int points, string reason)
            : this()
        {
            Check.Require(league != null, "league must be provided");
            Check.Require(team != null, "team must be provided");
            Check.Require(points > 0, "points must be greater than zero");
            Check.Require(!string.IsNullOrEmpty(reason) && reason.Trim() != string.Empty,
                "reason must be provided");

            this.League = league;
            this.Team = team;
            this.Points = points;
            this.Reason = reason;
        }

        public Penalty(League league, Team team, int points, string reason, Fixture fixture)
            : this(league, team, points, reason)
        {
            this.Fixture = fixture;
        }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual League League { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Team Team { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(200, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Reason { get; set; }

        //[Digits(1, Message=FormMessages.FieldMandatory)]
        [Range(1, 9999, ErrorMessage = FormMessages.FieldNumericGreaterThanZero)]
        public virtual int Points { get; set; }

        public virtual DateTime PenaltyDate { get; set; }

        /// <summary>
        /// Records the fixture for which the penalty was awarded. This is added purely so the automated
        /// task which checks for late match results can determine whether a penalty has already been 
        /// inserted for a particular fixture. For penalties entered manually this value will be null
        /// </summary>
        public virtual Fixture Fixture { get; set; }
    }
}
