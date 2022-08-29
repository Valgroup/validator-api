using Validator.Domain.Core.Models;

namespace Validator.Domain.Commands.Logins
{
    public class LoginResultCommand
    {
        public bool IsValid { get; set; } = true;
        public string Message { get; set; }
        public string Token { get; set; }
        public UsarioJwt Jwt { get; set; }
    }
}
