using System.ComponentModel.DataAnnotations.Schema;

namespace Basketball.Domain.Entities.ValueObjects
{
    [NotMapped]
    public class FirstRoundBye : Fixture
    {
        public FirstRoundBye()
        {
            CupRoundNo = 1;
        }
    }
}