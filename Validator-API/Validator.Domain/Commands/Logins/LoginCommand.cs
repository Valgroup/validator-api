namespace Validator.Domain.Commands.Logins
{
    public class LoginCommand
    {
        public Guid AdUserId { get; set; }
        public string Email { get; set; }
    }
}
