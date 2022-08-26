using Validator.Domain.Commands.Logins;

namespace Validator.Application.Interfaces
{
    public interface IAuthAppService
    {
        Task<LoginResultCommand> Autenticar(LoginCommand command);
    }
}
