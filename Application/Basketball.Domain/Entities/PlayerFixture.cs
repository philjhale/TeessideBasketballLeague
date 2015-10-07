using Basketball.Common.Validation;
using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Player stats for each fixture
    /// </summary>
    public class PlayerFixture : Entity
    {
        public PlayerFixture() 
        {
            IsMvp = "N";
        }

        public PlayerFixture(TeamLeague teamLeague, Fixture fixture, Player player, int pointsScored, int fouls)
            : this()
        {
            Check.Require(teamLeague != null, "teamLeague cannot be null");
            Check.Require(fixture != null, "teamLeague cannot be null");
            Check.Require(player != null, "player cannot be null");
            Check.Require(pointsScored >= 0, "pointsScored must be greater than or equal to zero");
            Check.Require(fouls >= 0, "fouls must be greater than or equal to zero");

            this.TeamLeague = teamLeague;
            this.Fixture = fixture;
            this.Player = player;
            this.PointsScored = pointsScored;
            this.Fouls = fouls;
        }

        // Can't include Required attributes on foreign keys seemingly because EF errors
        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual TeamLeague TeamLeague { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Fixture Fixture { get; set; }

        //[Required(ErrorMessage = FormMessages.FieldMandatory)]
        public virtual Player Player { get; set; }

        [Integer]
        // TODO Add Min attribute
        [Range(0, 999, ErrorMessage = FormMessages.FieldNumericGreaterThanZero)]
        public virtual int PointsScored { get; set; }

        [Integer]
        [Range(0, 5, ErrorMessage = FormMessages.MatchResultInvalidFoulRange)]
        public virtual int Fouls { get; set; }

        public virtual string IsMvp { get; set; }
		
		// TODO Add transient HasPlayed? [NotMapped]
		// Convert string Y/N to bool?
    }
}
