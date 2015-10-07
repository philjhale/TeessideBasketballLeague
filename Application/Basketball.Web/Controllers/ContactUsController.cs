using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Common.Resources;
using Basketball.Common.Util;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using System.Linq;

namespace Basketball.Web.Controllers
{
    public class ContactUsController : BaseController
    {
        private readonly IOptionService optionRepository;

        public ContactUsController(IOptionService optionRepository)
        {
            this.optionRepository = optionRepository;
        }

        #region Actions
        public ActionResult Index(string reason = null)
        {
            var model = new ContactUsViewModel();
            this.GetReasonFromQueryString(reason, model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactUsViewModel model)
        {
            List<string> emailAddresses = new List<string>();
            string subject = string.Empty;

            if (ModelState.IsValid)
            {

                switch (model.Reason)
                {
                    case ContactReason.AddPlayers:
                        emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_REGISTRAR));
                        emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_WEBADMIN));
                        subject = this.GetSubject(model.Name, "needs to add some players");
                        break;
                    case ContactReason.FoundBug:
                        emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_WEBADMIN));
                        subject = this.GetSubject(model.Name, "has found a bug");
                        break;
                    case ContactReason.NeedWebsiteHelp:
                        emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_WEBADMIN));
                        subject = this.GetSubject(model.Name, "has a question");
                        break;
                    case ContactReason.SomeOtherReason:
                        emailAddresses.AddRange(GetExecEmailAddresses());
                        subject = this.GetSubject(model.Name, "has submitted some feedback");
                        break;
                }

                Email emailHandler = new Email(false);
                
                // Split and add email addresses
                foreach (string emailAddress in emailAddresses)
                    emailHandler.AddToRecipient(emailAddress);

                try
                {
                    emailHandler.Send(subject, model.Message, model.Email);

                    return RedirectToAction("Sent");
                }
                catch (EmailSendException)
                {
                    TempData[FormMessages.MessageTypeFailure] = FormMessages.FeedbackSendError;
                }
            }
            return View(model);
        }

       

        public ActionResult Sent()
        {
            return View();
        } 

        public ActionResult AddPlayers()
        {
            return RedirectToAction("Index", new { reason = (int)ContactReason.AddPlayers });
        }

        public ActionResult AskForHelp()
        {
            return RedirectToAction("Index", new { reason = (int)ContactReason.NeedWebsiteHelp });
        }
        #endregion

        #region Private Methods
        private void GetReasonFromQueryString(string reason, ContactUsViewModel model)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                ContactReason contactReason;
                if (Enum.TryParse(reason, true, out contactReason))
                {
                    model.Reason = contactReason;
                }
            }
        }

        private IEnumerable<string> GetExecEmailAddresses()
        {
            List<string> emailAddresses = new List<string>();

            emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_CHAIRMAN));
            emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_DEVELOPMENT_AND_PRESS_OFFICER));
            emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_FIXURES_AND_REFS_OFFICER));
            emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_REGISTRAR));
            emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_TREASURER));
            emailAddresses.Add(optionRepository.GetByName(Option.EXEC_EMAIL_WEBADMIN));

            return emailAddresses.Where(x => !string.IsNullOrEmpty(x)).Distinct();
        }

        private string GetSubject(string name, string subject)
        {
            return string.Format("TBL message - {0} {1}", name, subject);
        }
        #endregion
    }
}