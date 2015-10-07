using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Basketball.Common.Extensions
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            Type type = enumObj.GetType();
            Array validValues = Enum.GetValues(type);

            var output = from Enum e in validValues select new { Id = e, Name = e.ToString() };

            return new SelectList(output, "Id", "Description", enumObj);
        }

    }
}
