using System;
using System.Linq;
using Basketball.Domain.Entities;

namespace Basketball.Data.Interfaces
{
    public partial interface IOptionRepository
    {
        Option GetByName(string optionName);
    }
}
