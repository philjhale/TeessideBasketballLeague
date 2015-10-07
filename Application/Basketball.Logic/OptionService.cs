using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Data;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Service.Interfaces;
using Basketball.Common.BaseTypes;

namespace Basketball.Service
{
    public partial class OptionService : BaseService<Option>, IOptionService
    {
        //private readonly IOptionRepository optionRepository;

        //public OptionService(IOptionRepository optionRepository) : base(optionRepository)
        //{
        //    this.optionRepository = optionRepository;
        //}

        public string GetByName(string optionName)
        {
            Option option;

            try
            {
                option = optionRepository.GetByName(optionName);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Required Option value does not exist. Add Option with the name '" + optionName + "'");
            }
            //Option option = Session.CreateCriteria(typeof(Option))
            //    .Add(Expression.Eq("Name", optionName))
            //    .UniqueResult<Option>();

            if (option == null)
                throw new ArgumentException("Required option value does not exit. Add Option with the name '" + optionName + "'");

            return option.Value;
        }
    }
}
