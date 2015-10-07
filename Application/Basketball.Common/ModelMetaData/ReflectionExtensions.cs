using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basketball.Common.ModelMetaData
{
    public static class ReflectionExtensions
    {
        public static bool PropertyExists(this Type type, string propertyName)
        {
            if (type == null || propertyName == null)
            {
                return false;
            }
            return type.GetProperty(propertyName) != null;
        }
    }
}
