using System;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace Basketball.Common.Util
{
    public class EmailSendException : Exception
    {
        public EmailSendException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Handles the creation and sending of emails
    /// 
    /// Requires three application settings to be defined in the Web.config file:
    ///     + EmailFrom
    ///     + EmailTestMode
    ///     + EmailTestAddress
    ///     
    /// If configuration value EmailTestMode is set to true all emails will be sent
    /// to the email address specified in the configuration value EmailTestAddress
    /// </summary>
    public class Email
    {
        private const string APP_EMAIL_FROM = "EmailFrom";
        private const string APP_EMAIL_TEST_MODE = "EmailTestMode";
        private const string APP_EMAIL_TEST_ADDRESS = "EmailTestAddress";

        private SmtpClient smptClient;
        private MailMessage message;
        private Boolean emailTestMode = false; // If this configuration value is true emails will only be sent to the emailTestAddress
        private string emailTestAddress;

        /// <summary>
        /// Create new MailMessage and SmtpClient objects and set the From field to the 
        /// specified configuration value
        /// </summary>
        /// <param name="htmlMode">Set to true if the email will contain HTML</param>
        public Email(Boolean htmlMode)
        {
            // Test for existence of required AppSettings
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[APP_EMAIL_FROM]))
                throw new ArgumentException("Email.Email() - required appSetting \"" + APP_EMAIL_FROM + "\" is missing from your web.config. Add the setting before using this class");
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[APP_EMAIL_TEST_MODE]))
                throw new ArgumentException("Email.Email() - required appSetting \"" + APP_EMAIL_TEST_MODE + "\" is missing from your web.config. Add the setting before using this class");
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[APP_EMAIL_TEST_ADDRESS]))
                throw new ArgumentException("Email.Email() - required appSetting \"" + APP_EMAIL_TEST_ADDRESS + "\" is missing from your web.config. Add the setting before using this class");

            // Create new mailmessage and smtpclient objects
            smptClient = new SmtpClient();
            message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings[APP_EMAIL_FROM]);
            message.IsBodyHtml = htmlMode;

            // Load application config variables
            if (ConfigurationManager.AppSettings[APP_EMAIL_TEST_MODE].ToString() == "true")
                emailTestMode = true;
            emailTestAddress = ConfigurationManager.AppSettings[APP_EMAIL_TEST_ADDRESS];
        }

        public Email(Boolean htmlMode, string recipient) : this(htmlMode)
        {
            AddToRecipient(recipient);
        }

        public Boolean HTMLMode
        {
            get { return message.IsBodyHtml; }
            set { message.IsBodyHtml = value; }
        }

        /// <exception cref="EmailSendException"></exception>
        public void Send(string subject, string body, string replyTo)
        {
            //message.From = new MailAddress(replyTo);
            message.ReplyToList.Add(replyTo);
            this.Send(subject, body);
        }

        /// <exception cref="EmailSendException"></exception>
        public void Send(string subject, string body)
        {
            #if !DEBUG
                message.Subject = subject;
                message.Body = body;

                Send();
            #endif
        }

        /// <summary>
        /// Sends the email. If the configuration option EmailTestMode is true
        /// all recipients are removed and replaced with EmailTestAddress address
        /// </summary>
        /// <exception cref="EmailSendException"></exception>
        private void Send()
        {
            // If the email test mode is on remove all recipients and add
            // the test email address
            if (emailTestMode)
            {
                message.To.Clear();
                message.CC.Clear();
                message.Bcc.Clear();
                AddToRecipient(emailTestAddress);
            }

            try
            {
                smptClient.Send(message);
            }
            catch (Exception ex)
            {
                throw new EmailSendException(ex.Message, ex.InnerException);
            }
        }

        public void AddToRecipient(string address)
        {
            message.To.Add(new MailAddress(address));
        }

        public void AddCcRecipient(string address)
        {
            message.CC.Add(new MailAddress(address));
        }

        public void AddBccRecipient(string address)
        {
            message.Bcc.Add(new MailAddress(address));
        }

        /// <summary>
        /// Attaches the specified file to the message
        /// </summary>
        /// <param name="filePath"></param>
        public void AttachFile(string filePath)
        {
            message.Attachments.Add(new Attachment(filePath));

        }

        /// <summary>
        /// Attaches a file via a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public void AttachFile(Stream stream, string fileName)
        {
            message.Attachments.Add(new Attachment(stream, fileName));
        }

        /// <summary>
        /// Prepare the Email object to send another email. Blank out the subject, message,
        /// recipients and remove attachments
        /// </summary>
        public void ResetEmail()
        {
            message.Subject = string.Empty;
            message.Body = string.Empty;
            message.To.Clear();
            message.Bcc.Clear();
            message.CC.Clear();
            message.Attachments.Clear();
        }
    }
}