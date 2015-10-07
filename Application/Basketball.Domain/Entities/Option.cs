using Basketball.Common.Resources;
using System.ComponentModel.DataAnnotations;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Values used in the configuration of the site. e.g. numnber of news articles
    /// displayed
    /// 
    /// Required values:
    /// 
    ///     HOME_NUM_NEWS_ARTICLES - Number of news articles displayed on home page
    ///     HOME_NUM_MATCH_REPORTS - Number of match reports displayed on the home page
    ///     HOME_NUM_EVENTS - Number of upcoming events shown on home page 
    ///     HOME_NUM_TICKER_PLAYER_STATS - Number of top scorers/worst foulers shown in ticker on home page
    ///     NEWS_NUM_ARTICLES - Number of news articles displayed on home page
    ///     FEEDBACK_EMAIL_ADDRESSES - The email addresses to which TBL feedback is sent
    ///     SCHEDULE_LATE_MATCH_RESULT_DAYS - The number of days after which a match result is considered late and a penalty is to be given. The is ONLY used as part of an automated process
    ///     SCHEDULE_LATE_RESULT_PEN_POINTS - The number of points a team will be penalised for a late match result. The is ONLY used as part of an automated process
    ///     
    /// </summary>
    public class Option : Entity
    {
        // Required values
        public static string HOME_NUM_NEWS_ARTICLES = "HOME_NUM_NEWS_ARTICLES";
        public static string HOME_NUM_MATCH_REPORTS = "HOME_NUM_MATCH_REPORTS";
        public static string HOME_NUM_EVENTS = "HOME_NUM_EVENTS";
        public static string HOME_NUM_TICKER_PLAYER_STATS = "HOME_NUM_TICKER_PLAYER_STATS";
        public static string NEWS_NUM_ARTICLES = "NEWS_NUM_ARTICLES";
        public static string SCHEDULE_LATE_MATCH_RESULT_DAYS = "SCHEDULE_LATE_MATCH_RESULT_DAYS";
        public static string SCHEDULE_LATE_RESULT_PEN_POINTS = "SCHEDULE_LATE_RESULT_PEN_POINTS";

        public static string EXEC_EMAIL_CHAIRMAN = "EXEC_EMAIL_CHAIRMAN";
        public static string EXEC_EMAIL_TREASURER = "EXEC_EMAIL_TREASURER";
        public static string EXEC_EMAIL_WEBADMIN = "EXEC_EMAIL_WEBADMIN";
        public static string EXEC_EMAIL_REGISTRAR = "EXEC_EMAIL_REGISTRAR";
        public static string EXEC_EMAIL_DEVELOPMENT_AND_PRESS_OFFICER = "EXEC_EMAIL_DEVELOPMENT_AND_PRESS_OFFICER";
        public static string EXEC_EMAIL_FIXURES_AND_REFS_OFFICER = "EXEC_EMAIL_FIXURES_AND_REFS_OFFICER";
        
        
        public Option() {}

        public Option(string name, string value)
            : this()
        {
            // Check for required values
            Check.Require(!string.IsNullOrEmpty(name) && name.Trim() != string.Empty, "name must be provided");
            Check.Require(!string.IsNullOrEmpty(value), "value must be provided");

            Name = name;
            Value = value;
        }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(40, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        [StringLength(100, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Value { get; set; }

        [StringLength(200, ErrorMessage = FormMessages.FieldTooLong)]
        public virtual string Description { get; set; }
    }
}
