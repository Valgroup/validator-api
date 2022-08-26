using Validator.Application.Interfaces;
using Validator.Domain.Commands.Logins;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Models;

namespace Validator.Application.Services
{
    public class AuthAppService : AppBaseService, IAuthAppService
    {
        public AuthAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<LoginResultCommand> Autenticar(LoginCommand command)
        {
            var jwt = new UsarioJwt()
            {
                Nome = "Jonata Gomes",
                Email = "jonata@valgroupco.com",
                Id = Guid.Parse("DD2FD0EC-F7BA-4252-8F6A-82F54FFBFB66"),
                Perfil = Domain.Core.Enums.EPerfilUsuario.Administrador,
                Permissao = new PermissaoJwt(),
                AnoBaseId = Guid.Parse("3C099378-2E67-480A-B0BB-900CB4B268EC")
               
            };
            return new LoginResultCommand { AccessToken = CryptoHelper.Crypto(jwt.Id.ToString()), Jwt = jwt };
        }
    }
}
