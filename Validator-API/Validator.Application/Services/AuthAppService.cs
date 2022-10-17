using Validator.Application.Interfaces;
using Validator.Domain.Commands.Logins;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Models;
using Validator.Domain.Dtos;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;
using Validator.Service.Sendgrid;

namespace Validator.Application.Services
{
    public class AuthAppService : AppBaseService, IAuthAppService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IProcessoService _processoService;
        private readonly IParametroService _parametroService;
        private readonly IUtilReadOnlyRepository _utilReadOnlyRepository;
        private readonly IUserResolver _userResolver;
        private readonly ISendGridService _sendgridService;
        private readonly ITemplateRazorService _templateRazorService;

        public AuthAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService,
            IProcessoService processoService,
            IUtilReadOnlyRepository utilReadOnlyRepository,
            IUserResolver userResolver,
            IParametroService parametroService,
            ISendGridService sendgridService,
            ITemplateRazorService templateRazorService) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
            _processoService = processoService;
            _utilReadOnlyRepository = utilReadOnlyRepository;
            _userResolver = userResolver;
            _parametroService = parametroService;
            _sendgridService = sendgridService;
            _templateRazorService = templateRazorService;
        }

        public async Task<LoginResultCommand> Autenticar(LoginCommand command)
        {
            var usuario = await _usuarioService.Find(f => f.Email == command.Email && f.Ativo);
            if (usuario == null)
                return new LoginResultCommand { IsValid = false, Message = "Usuário ou senha inválidos" };

            if (!usuario.Autenticar(command.Senha))
                return new LoginResultCommand { IsValid = false, Message = "Usuário ou senha inválidos" };


            bool liberaProcesso = false;
            var processo = await _processoService.GetByCurrentYear();
            if (processo != null && processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.SemPendencia)
                liberaProcesso = true;

            var parametros = await _parametroService.GetByCurrentYear();

            if (parametros != null && usuario.Perfil != EPerfilUsuario.Administrador)
            {
                var dh = DateTime.Now;
                var dhHoje = new DateTime(dh.Year, dh.Month, dh.Day, 23, 59, 50);
                if (parametros.DhFinalizacao < dhHoje)
                    return new LoginResultCommand { IsValid = false, Message = $"Oops... A etapa de escolha finalizou em {parametros.DhFinalizacao.ToShortDateString()}! Caso tenha alguma demanda sobre este assunto, acione o RH!" };
            }

            var download = await _utilReadOnlyRepository.TemDadosExportacao();

            var jwt = new UsarioJwt()
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                EmailSuperior = usuario.EmailSuperior,
                NomeSuperior = usuario?.Superior?.Nome,
                Id = usuario.Id,
                Perfil = usuario.Perfil,
                Permissao = new PermissaoJwt(),
                AnoBaseId = usuario.AnoBaseId,
                DivisaoNome = usuario?.Divisao?.Nome,
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

        public async Task<ValidationResult> RecuperarSenha(string email)
        {
            var usuario = await _usuarioService.Find(f => f.Email == email && f.Ativo);
            if (usuario == null)
            {
                ValidationResult.Add("E-mail não encontrado");
                return ValidationResult;
            }

            var parametros = await _parametroService.GetByCurrentYear();

            if (parametros != null && usuario.Perfil != EPerfilUsuario.Administrador)
            {
                var dh = DateTime.Now;
                var dhHoje = new DateTime(dh.Year, dh.Month, dh.Day, 23, 59, 50);
                if (parametros.DhFinalizacao < dhHoje)
                {
                    ValidationResult.Add($"Oops... A etapa de escolha finalizou em {parametros.DhFinalizacao.ToShortDateString()}! Caso tenha alguma demanda sobre este assunto, acione o RH!");
                    return ValidationResult;
                }
            }

            var novaSenha = usuario.MudarSenha();

            _usuarioService.Update(usuario);

            await CommitAsync();

            var emailDto = new EmailAcessoDto
            {
                Nome = usuario.Nome,
                Senha = novaSenha,
                Login = usuario.Email,
                Prazo = parametros != null ? parametros.DhFinalizacao.ToShortDateString() : new DateTime(2022, 10, 28).ToShortDateString(),
                Link = RuntimeConfigurationHelper.UrlApp
            };

            var html = await _templateRazorService.BuilderHtmlAsString("Email/_EnvioAcesso", emailDto);

            await _sendgridService.SendAsync(usuario.Nome, email, html, "Recuperação de Senha");

            return ValidationResult;
        }

        public async Task<PermissaoJwt> Permissao(Usuario? usuario = null)
        {
            bool liberaProcesso = false;
            bool habilitarParametros = true;
            bool liberarDocumento = true;
            var processo = await _processoService.GetByCurrentYear();
            if (processo != null && processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.SemPendencia)
                liberaProcesso = true;

            if (processo != null && processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.Inicializada)
            {
                liberarDocumento = false;
                habilitarParametros = false;
            }

            EPerfilUsuario perfil;
            Guid usuarioId;
            if (usuario == null)
            {
                var user = await _userResolver.GetAuthenticateAsync();
                perfil = user.Perfil;
                usuarioId = user.Id;
            }
            else
            {
                perfil = usuario.Perfil;
                usuarioId = usuario.Id;
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
                        Documento = liberarDocumento,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = true,
                        ConsutarUsuarios = true,
                        HabilitarParametros = habilitarParametros,
                        Download = download,
                        QtdPendentes = await _utilReadOnlyRepository.ObterQtdPendetes(),
                        QtdTotal = await _utilReadOnlyRepository.ObterQtdTotal()
                    };

                case Domain.Core.Enums.EPerfilUsuario.Avaliado:
                    return new PermissaoJwt()
                    {
                        Documento = false,
                        LiberarProcesso = liberaProcesso,
                        LimparBase = false,
                        ConsutarUsuarios = false,
                        HabilitarParametros = false,
                        SugestaoEnviada = await _utilReadOnlyRepository.TemSugestaoEnviadas(usuarioId)
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
                        HabilitarParametros = false,
                        SugestaoEnviada = await _utilReadOnlyRepository.TemSugestaoEnviadas(usuarioId)
                    };

                default:
                    return new PermissaoJwt();

            }
        }

        public async Task<ValidationResult> AdicionarAdm(UsuarioAdministradorCommand command)
        {
            var anoBase = await _utilReadOnlyRepository.ObterAnoBae();
            var usuario = new Usuario(Guid.NewGuid(), command.Nome, command.Email, null, false, "RH", null, false);
            usuario.AnoBaseId = anoBase.AnoBaseId;

            usuario.EhAdministrador();

            await _usuarioService.CreateAsync(usuario);

            await CommitAsync();

            //var parametros = await _parametroService.GetByCurrentYear();

            var emailDto = new EmailAcessoDto
            {
                Nome = usuario.Nome,
                Senha = usuario.SenhaGerada(),
                Login = usuario.Email,
                Prazo = new DateTime(2022, 10, 28).ToShortDateString(),
                Link = RuntimeConfigurationHelper.UrlApp
            };

            var html = await _templateRazorService.BuilderHtmlAsString("Email/_EnvioAcesso", emailDto);

            await _sendgridService.SendAsync(usuario.Nome, command.Email, html, "Acesso de Administrador");

            return ValidationResult;
        }
    }
}
