using Validator.Application.Emails;
using Validator.Application.Interfaces;
using Validator.Data.Dapper;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos;
using Validator.Domain.Dtos.Dashes;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;
using Validator.Service.Sendgrid;

namespace Validator.Application.Services
{
    public class DashAppService : AppBaseService, IDashAppService
    {
        private readonly IParametroService _parametroService;
        private readonly IProcessoService _processoService;
        private readonly IDashReadOnlyRepository _dashReadOnlyRepository;
        private readonly IPlanilhaReadOnlyRepository _planilhaReadOnlyRepository;
        private readonly IDivisaoService _divisaoService;
        private readonly ISetorService _setorService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly ITemplateRazorService _templateRazorService;
        private readonly IEmailService _emailService;
        private readonly IUserResolver _userResolver;
        private readonly ISendGridService _sendgridService;

        public DashAppService(IUnitOfWork unitOfWork, IParametroService parametroService, IDashReadOnlyRepository dashReadOnlyRepository,
            IProcessoService processoService, IPlanilhaReadOnlyRepository planilhaReadOnlyRepository, IDivisaoService divisaoService,
            ISetorService setorService, IUsuarioService usuarioService, IUsuarioReadOnlyRepository usuarioReadOnlyRepository,
            ITemplateRazorService templateRazorService,
            IEmailService emailService, IUserResolver userResolver, ISendGridService sendgridService) : base(unitOfWork)
        {
            _parametroService = parametroService;
            _dashReadOnlyRepository = dashReadOnlyRepository;
            _processoService = processoService;
            _planilhaReadOnlyRepository = planilhaReadOnlyRepository;
            _divisaoService = divisaoService;
            _setorService = setorService;
            _usuarioService = usuarioService;
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _templateRazorService = templateRazorService;
            _emailService = emailService;
            _userResolver = userResolver;
            _sendgridService = sendgridService;
        }

        public async Task<ValidationResult> AdicionarOuAtualizar(ParametroSalvarCommand command)
        {
            if (command.QtdeSugestaoMax == 0 || command.QtdeSugestaoMin == 0 || command.QtdeAvaliador == 0)
            {
                return ValidationResult;
            }

            if (command.QtdeSugestaoMin > command.QtdeSugestaoMax)
            {
                ValidationResult.Add("Quantidade Mínima não pode ser maior que a Quantidade Máxima");
                return ValidationResult;
            }

            if (command.QtdeAvaliador < command.QtdeSugestaoMin)
            {
                ValidationResult.Add("Quantidade Avaliador não pode ser menor do que a Quantidade Mínima de Sugestão");
                return ValidationResult;
            }

            if (command.QtdeAvaliador > command.QtdeSugestaoMax)
            {
                ValidationResult.Add("Quantidade Avaliador não pode ser maior do que a Quantidade Máxima de Sugestão");
                return ValidationResult;
            }

            if (command.DhFinalizacao.HasValue)
            {
                var dh = DateTime.Now;
                var df = command.DhFinalizacao.Value;
                var dhFinal = new DateTime(df.Year, df.Month, df.Day);
                if (dhFinal.Date < dh.Date)
                {
                    ValidationResult.Add("A data da finalização do processo não poder menor do que o dia de hoje!");
                    return ValidationResult;
                }
            }

            var parametro = await _parametroService.GetByCurrentYear();
            if (parametro != null)
            {
                parametro.Editar(command.QtdeAvaliador, command.QtdeSugestaoMin, command.QtdeSugestaoMax, command.DhFinalizacao);
                ValidationResult.Add(_parametroService.Update(parametro));
            }
            else
            {
                parametro = new Parametro(command.QtdeSugestaoMin, command.QtdeSugestaoMax, command.QtdeAvaliador, command.DhFinalizacao);
                ValidationResult.Add(await _parametroService.CreateAsync(parametro));
            }

            if (ValidationResult.IsValid)
                await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> ExcluirProcessoAnoAtual()
        {
            var user = await _userResolver.GetAuthenticateAsync();
            if (user != null && user.Perfil == Domain.Core.Enums.EPerfilUsuario.Administrador)
            {
                await _planilhaReadOnlyRepository.ExcluirProcessoAnoAtual(user.AnoBaseId);
            }
            else
            {
                ValidationResult.Add("Sem permissão");
            }
            return ValidationResult;
        }

        public async Task<ValidationResult> IniciarProcesso(string url)
        {

           // await EnvairEmailAcesso(new List<Usuario> { new Usuario(Guid.NewGuid(), "João Ricardo Recanello", "joao.recanello_ext@valgrouco.com", "", false, "", "valgroup2022", "11111") }, url);

            var processo = await _processoService.GetByCurrentYear();
            if (processo == null)
            {
                ValidationResult.Add("Não existe nenhum processamento!");
                return ValidationResult;
            }

            if (processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.ComPendencia)
            {
                ValidationResult.Add("Possui pendências para dar inicio no processo!");
                return ValidationResult;
            }

            if (processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.Inicializada)
            {
                ValidationResult.Add("O processo já foi inicializado!");
                return ValidationResult;
            }

            if (processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.Finalizado)
            {
                ValidationResult.Add("O processo já está finalizado!");
                return ValidationResult;
            }

            var parametros = await _parametroService.GetByCurrentYear();
            if (parametros == null)
            {
                ValidationResult.Add("Não existe parâmetros para iniciar o Processo!");
                return ValidationResult;
            }

            if (parametros.QtdeSugestaoMin == 0)
            {
                ValidationResult.Add("Quantidade Miníma do paramêtro tem que ser maior que zero!");
                return ValidationResult;
            }

            if (parametros.QtdeSugestaoMax == 0)
            {
                ValidationResult.Add("Quantidade Máxima do paramêtro tem que ser maior que zero!");
                return ValidationResult;
            }

            if (parametros.QtdeAvaliador == 0)
            {
                ValidationResult.Add("Quantidade de Avaliadores do paramêtro tem que ser maior que zero!");
                return ValidationResult;
            }


            var usuariosExistentes = await _usuarioReadOnlyRepository.TodosPorAno();
            var planilhas = await _planilhaReadOnlyRepository.ObterTodas();
            var divisoesExistentes = await _divisaoService.FindAllByYear();
            var divisoes = new List<Divisao>();
            var unidades = planilhas.Select(s => s.Unidade).DistinctBy(s => s);
            foreach (var nome in unidades)
            {
                var existe = divisoesExistentes.FirstOrDefault(f => f.Nome.Contains(nome));
                if (existe != null)
                {
                    divisoes.Add(existe);
                    continue;
                }

                var entDiv = new Divisao(nome);
                divisoes.Add(entDiv);
                await _divisaoService.CreateAsync(entDiv);
            }

            var setoresExistentes = await _setorService.FindAllByYear();
            var setores = new List<Setor>();
            var setoresOuNiveis = planilhas.Select(s => s.CentroCusto).DistinctBy(s => s);
            foreach (var centroCusto in setoresOuNiveis)
            {
                var existe = setoresExistentes.FirstOrDefault(f => f.Nome.Contains(centroCusto));
                if (existe != null)
                {
                    setores.Add(existe);
                    continue;
                }

                var entSet = new Setor(centroCusto);
                setores.Add(entSet);
                await _setorService.CreateAsync(entSet);
            }

            var usuarios = new List<Usuario>();

            foreach (var linha in planilhas)
            {
                var usuarioExiste = usuariosExistentes.FirstOrDefault(f => f.Email == linha.Email);
                if (usuarioExiste != null)
                    continue;

                var ehDiretor = !string.IsNullOrEmpty(linha.Direcao) && linha.Direcao.Contains('x');
                var usuario = new Usuario(Guid.NewGuid(), linha.Nome, linha.Email, linha.EmailSuperior, ehDiretor, linha.Nivel, "valgroup2022", linha.CPF);

                var setorId = setores.First(f => f.Nome == linha.CentroCusto).Id;
                var divisaoId = divisoes.First(f => f.Nome == linha.Unidade).Id;
                usuario.InformarDadosExtras(setorId, divisaoId);

                usuarios.Add(usuario);

                await _usuarioService.CreateAsync(usuario);

            }

            await CommitAsync();

            // await EnvairEmailAcesso(usuarios, url);

            foreach (var usuario in usuarios)
            {
                var superior = usuarios.FirstOrDefault(f => f.Email == usuario.EmailSuperior);
                var existeComoSuperior = usuarios.Any(a => a.EmailSuperior == usuario.Email);

                usuario.ExecutarRegraPerfil(existeComoSuperior, superior);

                _usuarioService.Update(usuario);
            }

            processo.Inicializar();

            _processoService.Update(processo);

            await CommitAsync();

            return ValidationResult;

        }

        public async Task<ParametroDto> ObterParametros()
        {
            return await _dashReadOnlyRepository.ObterParametros();
        }

        public async Task<PermissaoDto> ObterPermissao()
        {
            return new PermissaoDto();
        }

        public async Task<DashResultadosDto> ObterResultados(ConsultarResultadoCommand command)
        {
            return await _dashReadOnlyRepository.ObterResultados(command);
        }

        private async Task EnvairEmailAcesso(List<Usuario> usuarios, string url)
        {
            var parametro = await _parametroService.GetByCurrentYear();

            foreach (var usuario in usuarios)
            {
                var emailDto = new EmailAcessoDto
                {
                    Nome = usuario.Nome,
                    Senha = usuario.Id.ToString().Split("-")[0].ToLower(),
                    Login = usuario.Email,
                    Prazo = parametro.DhFinalizacao.ToShortDateString(),
                    Link = url
                };

                var html = await _templateRazorService.BuilderHtmlAsString("Email/_EnvioAcesso", emailDto);

                await _sendgridService.SendAsync(usuario.Nome, "joao.recanello_ext@valgroupco.com", html, "Escolha dos Avaliadores");
                break;

            }
        }
    }
}
