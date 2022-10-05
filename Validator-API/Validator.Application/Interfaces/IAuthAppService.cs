using Validator.Domain.Commands.Logins;
using Validator.Domain.Core;
using Validator.Domain.Core.Models;
using Validator.Domain.Entities;

namespace Validator.Application.Interfaces
{
    public interface IAuthAppService
    {
        Task<LoginResultCommand> Autenticar(LoginCommand command);
        Task<PermissaoJwt> Permissao(Usuario? usuario = null);
        Task<ValidationResult> RecuperarSenha(string email, string url);
    }
}
