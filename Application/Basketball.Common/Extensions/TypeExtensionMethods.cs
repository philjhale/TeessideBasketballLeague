using System;
using System.Collections.Generic;
using System.Reflection;
using Basketball.Common.Domain;

namespace Basketball.Common.Extensions
{
    public static class TypeExtensionMethods
    {
        public static List<string> GetNavigationProperties(this Type typeToSearch)
        {
            PropertyInfo[] propertyInfo = typeToSearch.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> navigationProperties = new List<string>();
            foreach (var property in propertyInfo)
            {
                if (property.PropertyType.BaseType != null && property.PropertyType.BaseType == typeof(Entity))
                {
                    navigationProperties.Add(property.Name);
                }
            }

            return navigationProperties;
        }
    }
}
