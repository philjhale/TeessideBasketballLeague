using System.ComponentModel.DataAnnotations.Schema;

namespace Basketball.Domain.Entities.ValueObjects
{
    [NotMapped]
    public class FakeCupFixture : Fixture
    {
        public FakeCupFixture(int cupRoundNumber)
        {
            this.CupRoundNo = cupRoundNumber;
        }
    }
}