namespace Basketball.Domain.Entities.ValueObjects
{
    public class RefereeWithCurrentSeasonFixtureCount
    {
        public Referee Referee { get; set; }
        public int NumberOfFixturesRefereedThisSeason { get; set; }

        public RefereeWithCurrentSeasonFixtureCount(Referee referee, int numberOfFixturesRefereedThisSeason)
        {
            this.Referee = referee;
            this.NumberOfFixturesRefereedThisSeason = numberOfFixturesRefereedThisSeason;
        }
    }
}
