using Validator.Application.Interfaces;
using Validator.Domain.Commands.Logins;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Models;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Application.Services
{
    public class AuthAppService : AppBaseService, IAuthAppService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IProcessoService _processoService;
        private readonly IUtilReadOnlyRepository _utilReadOnlyRepository;
        private readonly IUserResolver _userResolver;
        public AuthAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService,
            IProcessoService processoService,
            IUtilReadOnlyRepository utilReadOnlyRepository, IUserResolver userResolver) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
            _processoService = processoService;
            _utilReadOnlyRepository = utilReadOnlyRepository;
            _userResolver = userResolver;
        }

        public async Task<LoginResultCommand> Autenticar(LoginCommand command)
        {
            var usuario = await _usuarioService.Find(f => f.Email == command.Email);
            if (usuario == null)
                return new LoginResultCommand { IsValid = false, Message = "Usuário ou senha inválidos" };

            if (!usuario.Autenticar(command.Senha))
                return new LoginResultCommand { IsValid = false, Message = "Usuário ou senha inválidos" };

            if (usuario.Deleted)
                return new LoginResultCommand { IsValid = false, Message = "Usuário está inativo" };

            bool liberaProcesso = false;
            var processo = await _processoService.GetByCurrentYear();
            if (processo != null && processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.SemPendencia)
                liberaProcesso = true;

            var download = await _utilReadOnlyRepository.TemDadosExportacao();

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

            jwt.Permissao = await Permissao(usuario);

            //switch (usuario.Perfil)
            //{
            //    case Domain.Core.Enums.EPerfilUsuario.Diretor:
            //        jwt.Permissao = new PermissaoJwt()
            //        {
            //            Documento = false,
            //            LiberarProcesso = liberaProcesso,
            //            LimparBase = false,
            //            ConsutarUsuarios = true,
            //            HabilitarParametros = false

            //        };
            //        break;
            //    case Domain.Core.Enums.EPerfilUsuario.Administrador:
            //        jwt.Permissao = new PermissaoJwt()
            //        {
            //            Documento = liberaProcesso,
            //            LiberarProcesso = liberaProcesso,
            //            LimparBase = false,
            //            ConsutarUsuarios = true,
            //            HabilitarParametros = !liberaProcesso,
            //            Download = download
            //        };
            //        break;
            //    case Domain.Core.Enums.EPerfilUsuario.Avaliado:
            //        jwt.Permissao = new PermissaoJwt()
            //        {
            //            Documento = false,
            //            LiberarProcesso = liberaProcesso,
            //            LimparBase = false,
            //            ConsutarUsuarios = false,
            //            HabilitarParametros = false
            //        };
            //        break;
            //    case Domain.Core.Enums.EPerfilUsuario.Aprovador:
            //        jwt.Permissao = new PermissaoJwt()
            //        {
            //            Documento = false,
            //            LiberarProcesso = liberaProcesso,
            //            LimparBase = false,
            //            ConsutarUsuarios = false,
            //            HabilitarParametros = false
            //        };
            //        break;
            //    case Domain.Core.Enums.EPerfilUsuario.Ambos:
            //        jwt.Permissao = new PermissaoJwt()
            //        {
            //            Documento = false,
            //            LiberarProcesso = liberaProcesso,
            //            LimparBase = false,
            //            ConsutarUsuarios = false,
            //            HabilitarParametros = false
            //        };
            //        break;
            //    default:
            //        break;
            //}

            return new LoginResultCommand { Token = CryptoHelper.Crypto(jwt.Id.ToString()), Jwt = jwt };
        }

        public async Task<PermissaoJwt> Permissao(Usuario? usuario = null)
        {
            bool liberaProcesso = false;
            var processo = await _processoService.GetByCurrentYear();
            if (processo != null && processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.SemPendencia)
                liberaProcesso = true;

            EPerfilUsuario perfil;
            if (usuario == null)
            {
                var user = await _userResolver.GetAuthenticateAsync();
                perfil = user.Perfil;
            }
            else
            {
                perfil = usuario.Perfil;
            }

            var download = await _utilReadOnlyRepository.TemDadosExportacao();

            switch (perfil)
            {
                case Domain.Core.Enums.EPerfilUsuario.Diretor:
                    return new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = true,
                        HabilitarParametros = false

                    };

                case Domain.Core.Enums.EPerfilUsuario.Administrador:
                    return new PermissaoJwt()
                    {
                        Documento = liberaProcesso,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = true,
                        HabilitarParametros = !liberaProcesso,
                        Download = download
                    };

                case Domain.Core.Enums.EPerfilUsuario.Avaliado:
                    return new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = false,
                        HabilitarParametros = false
                    };

                case Domain.Core.Enums.EPerfilUsuario.Aprovador:
                    return new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = false,
                        HabilitarParametros = false
                    };

                case Domain.Core.Enums.EPerfilUsuario.Ambos:
                    return new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = false,
                        HabilitarParametros = false
                    };

                default:
                    return new PermissaoJwt();

            }
        }
    }
}
