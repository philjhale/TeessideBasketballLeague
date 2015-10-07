using Basketball.Common.Mapping;

namespace Basketball.Domain.Entities.ValueObjects
{
    // This may be a slightly ropey idea but the distinction between a ViewObject and a ValueObject
    // is that a ViewObject exists purely to display stuff to the user whereas a ValueObject can
    // also be used in the service layer
    public class PlayerFixtureStats
    {
        public int      PlayerFixtureId { get; set; }
        public int      PlayerId        { get; set; }
        public string   Name            { get; set; }
        public int      PointsScored    { get; set; }
        public int      Fouls           { get; set; }
        public bool     HasPlayed       { get; set; }
        public bool     IsMvp           { get; set; }

        public PlayerFixtureStats()
        {
        }

        public PlayerFixtureStats(PlayerFixture playerFixture, bool hasPlayed)
        {
            MapToModel(playerFixture);
            this.HasPlayed = hasPlayed;
        }

        public void MapToModel(PlayerFixture playerFixture)
        {
            this.PlayerFixtureId = playerFixture.Id;
            this.PlayerId        = playerFixture.Player.Id;
            this.Name            = playerFixture.Player.ToString();
            this.PointsScored    = playerFixture.PointsScored;
            this.Fouls           = playerFixture.Fouls;
            this.IsMvp           = playerFixture.IsMvp.YesNoToBool();
        }

        public PlayerFixture MapToPlayerFixture(PlayerFixture playerFixture)
        {
            playerFixture.PointsScored = this.PointsScored;
            playerFixture.Fouls        = this.Fouls;
            playerFixture.IsMvp        = this.IsMvp.BoolToYesNo();

            return playerFixture;
        }
    }
}
