using Validator.Domain.Core.Models;

namespace Validator.Domain.Commands.Logins
{
    public class LoginResultCommand
    {
        public bool IsValid { get; set; } = true;
        public string Mesagem { get; set; }
        public string AccessToken { get; set; }
        public UsarioJwt Jwt { get; set; }
    }
}
