using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Application.Emails
{
    public class SendgridService : ISendgridService
    {
        private string _token;
        private readonly SendGridClient _sendGridClient;
        public SendgridService()
        {
            _token = "";
            _sendGridClient = new SendGridClient("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");
        }
        public async Task Send(string html, string email, string nome)
        {
            var from = new EmailAddress("nao-responder");
        }
    }
}
