using Basketball.Domain.Entities;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Data
{
    public partial class OptionRepository : BaseRepository<Option>, IOptionRepository
    {
        public Option GetByName(string optionName)
        {
            return (from o in Get() where o.Name == optionName select o).Single();
        }
    }
}
