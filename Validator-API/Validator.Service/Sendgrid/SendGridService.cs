using SendGrid;
using SendGrid.Helpers.Mail;

namespace Validator.Service.Sendgrid
{
    public class SendGridService : ISendGridService
    {
        private readonly SendGridClient _sendGridClient;

        public SendGridService()
        {
            _sendGridClient = new SendGridClient("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");
        }

        public async Task SendAsync(string name, string emailTo, string html, string subject)
        {
            var from = new EmailAddress("nao-responder@valgroupco.com", "Valgroup TI");
            var to = new EmailAddress(emailTo, name);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, html, html);

            var response = await _sendGridClient.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {

            }

        }
    }
}
