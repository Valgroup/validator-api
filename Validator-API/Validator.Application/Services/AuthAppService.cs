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
        private readonly IProcessoService _processoService;
        public AuthAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService, IProcessoService processoService) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
            _processoService = processoService;
        }

        public async Task<LoginResultCommand> Autenticar(LoginCommand command)
        {
            var usuario = await _usuarioService.Find(f => f.Email == command.Email);
            if (usuario == null)
                return new LoginResultCommand { IsValid = false, Message = "Usuário ou senha inválidos" };

            if (!usuario.Autenticar(command.Senha))
                return new LoginResultCommand { IsValid = false, Message = "Usuário ou senha inválidos" };

            bool liberaProcesso = false;
            var processo = await _processoService.GetByCurrentYear();
            if (processo != null && processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.SemPendencia)
                liberaProcesso = true;

            var jwt = new UsarioJwt()
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Id = usuario.Id,
                Perfil = usuario.Perfil,
                Permissao = new PermissaoJwt(),
                AnoBaseId = usuario.AnoBaseId,
                DivisaoNome = usuario.Divisao.Nome,
                DivisaoId = usuario.DivisaoId
            };

            switch (usuario.Perfil)
            {
                case Domain.Core.Enums.EPerfilUsuario.Diretor:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = true
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Administrador:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = liberaProcesso,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = true
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Avaliado:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = false
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Aprovador:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = false
                    };
                    break;
                case Domain.Core.Enums.EPerfilUsuario.Ambos:
                    jwt.Permissao = new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
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
