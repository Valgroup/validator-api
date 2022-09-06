using Validator.Application.Interfaces;
using Validator.Domain.Commands.Logins;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Models;
using Validator.Domain.Interfaces;

namespace Validator.Application.Services
{
    public class AuthAppService : AppBaseService, IAuthAppService
    {
        private readonly IUsuarioService _usuarioService;
        public AuthAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
        }

        public async Task<LoginResultCommand> Autenticar(LoginCommand command)
        {
            var usuario = await _usuarioService.Find(f => f.Email == command.Email);
            if (usuario == null)
                return new LoginResultCommand { IsValid = false, Message = "Usuário inválido" };

            var jwt = new UsarioJwt()
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Id = usuario.Id,
                Perfil = usuario.Perfil,
                Permissao = new PermissaoJwt(),
                AnoBaseId = usuario.AnoBaseId

            };

            switch (usuario.Perfil)
            {
                case Domain.Core.Enums.EPerfilUsuario.Diretor:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = false,
                        LimparBase = false,
                        ConsutarUsuarios = true
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Administrador:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = true,
                        LiberarProcesso = true,
                        LimparBase = false,
                        ConsutarUsuarios = true
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Avaliado:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = false,
                        LimparBase = false,
                        ConsutarUsuarios = false
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Aprovador:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = false,
                        LimparBase = false,
                        ConsutarUsuarios = false
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Ambos:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = false,
                        LimparBase = false,
                        ConsutarUsuarios = false
                    };
                    break;
                default:
                    break;
            }

            return new LoginResultCommand { Token = CryptoHelper.Crypto(jwt.Id.ToString()), Jwt = jwt };
        }
    }
}
