using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Stores team information in relation to a league.
    /// .i.e. league points, games won etc. 
    /// </summary>
    public class TeamLeague : Entity
    {
        public TeamLeague() { }

        public TeamLeague(League league, Team team, string teamName, string teamNameLong)
            : this()
        {
            Check.Require(league != null, "league must be provided");
            Check.Require(team != null, "team must be provided");
            Check.Require(!string.IsNullOrEmpty(teamName) && teamName.Trim() != string.Empty,
                            "teamName must be provided");
            Check.Require(!string.IsNullOrEmpty(teamNameLong) && teamNameLong.Trim() != string.Empty,
                            "teamName must be provided");


            this.League = league;
            this.Team = team;
            this.TeamName = teamName;
            this.TeamNameLong = teamNameLong;
        }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual League League { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Team Team { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(20, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string TeamName { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(50, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string TeamNameLong { get; set; }

        /// <summary>
        /// Number of games won or lost consecutively.e.g. W2 or L1
        /// </summary>
        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(3, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Streak { get; set; }

        // All the properites below should have defaults set on the database
        // so the validation attributes shouldn't really be required. However
        // I've stuck them in just in case
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesWonTotal { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesLostTotal { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual decimal GamesPct { get; set; }
    
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesWonHome { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesLostHome { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesWonAway { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesLostAway { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesPlayed { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int PointsLeague { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int PointsPenalty { get; set; } // Total penalty points for a team

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int GamesForfeited { get; set; } 

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int PointsScoredFor { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int PointsScoredAgainst { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual int PointsScoredDifference{ get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual decimal PointsScoredPerGameAvg { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual decimal PointsAgainstPerGameAvg { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual decimal PointsScoredPerGameAvgDifference { get; set; }
    }
}
