namespace Basketball.Common.Mapping
{
    public static class CommonMapping
    {
        // Arguably this should be in StringExtensions
        public static bool YesNoToBool(this string yesNo)
        {
            if(!string.IsNullOrEmpty(yesNo))
            {
                if (yesNo.ToLower() == "y" || yesNo.ToLower() == "yes")
                    return true;
            }

            return false;
        }

        public static string BoolToYesNo(this bool yesNo)
        {
            return yesNo ? "Y" : "N";
        }
    }
}
