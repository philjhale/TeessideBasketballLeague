using System.Text.RegularExpressions;

namespace Basketball.Common.Extensions
{
    public static class StringExtensions
    {
        public static string SimplePluralise(this string str)
        {
            string lastCharacter = str[str.Length-1].ToString();

            if (lastCharacter == "y")
                return str.Substring(0, str.Length - 1) + "ies";
            else if (lastCharacter == "s")
                return str;
            else
                return str + "s";
        }

        public static string FirstCharToLower(this string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);

        }

        public static string IsDayOfWeek(this string domainClass)
        {
            return  (domainClass == "DayOfWeek" ? "Basketball.Domain.Entities.DayOfWeek" : domainClass);
        }

        /// <summary>
        /// E.g. Converts "MyTeamName" to "My Team Name"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddSpacesAfterCapitals(this string str)
        {
            str = Regex.Replace(str, "([A-Z])", " $1");
            str = str.Substring(1, str.Length - 1);
            return str;
        }
		
		public static string AddDayOfMonthSuffix(this int dayOfMonth)
		{
            string suffix = "th";

            if (dayOfMonth == 1 || dayOfMonth == 21 || dayOfMonth == 31)
                suffix = "st";
            else if (dayOfMonth == 2 || dayOfMonth == 22)
                suffix = "nd";
            else if (dayOfMonth == 3 || dayOfMonth == 23)
                suffix = "rd";

            return dayOfMonth + suffix;
		}
    }
}
