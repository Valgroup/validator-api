using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Application.Emails
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        public EmailService()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }
        public async Task SendAsync(string html, string email, string subject)
        {
            var content = new Dictionary<string, string>()
            {
                {"Emails", email },
                {"Subject", subject },
                {"Body", html }
            };

            var response = await _httpClient.PostAsync("http://matera:8213/Home/EnviaEmail", new FormUrlEncodedContent(content));
            if (response.IsSuccessStatusCode)
            {

            }

        }
    }
}
