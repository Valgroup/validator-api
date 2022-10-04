using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Service.Sendgrid
{
    public interface ISendGridService
    {
        Task SendAsync(string name, string emailTo, string html, string subject);
    }
}
