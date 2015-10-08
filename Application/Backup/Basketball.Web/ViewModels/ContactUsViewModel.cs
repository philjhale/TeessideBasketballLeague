using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Common.Validation;

namespace Basketball.Web.ViewModels
{
    public class ContactUsViewModel
    {
        [UIHint("ContactReason")]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public ContactReason Reason { get; set; }

        public List<SelectListItem> Reasons { get { return GetReasonList(); } }
        
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string Name { get; set; }

        [Email(ErrorMessage = FormMessages.FieldEmailInvalid)]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string Email { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public string Message { get; set; }

        private List<SelectListItem> GetReasonList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "I need to add some players to the site", Value = ((int)ContactReason.AddPlayers).ToString()});
            list.Add(new SelectListItem() { Text = "I've found a bug", Value = ((int)ContactReason.FoundBug).ToString() });
            list.Add(new SelectListItem() { Text = "I need some help with the website or iPhone app", Value = ((int)ContactReason.NeedWebsiteHelp).ToString() });
            list.Add(new SelectListItem() { Text = "Some other reason", Value = ((int)ContactReason.SomeOtherReason).ToString() });
            return list.ToSelectListWithHeader(x => x.Text, x => x.Value, null).OrderBy(x => x.Value).ToList();
        }
    }

    public enum ContactReason
    {
        AddPlayers = 1,
        FoundBug,
        NeedWebsiteHelp,
        SomeOtherReason
    }
}
