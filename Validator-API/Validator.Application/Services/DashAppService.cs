using Validator.Application.Interfaces;
using Validator.Data.Dapper;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos.Dashes;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;

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
        public DashAppService(IUnitOfWork unitOfWork, IParametroService parametroService, IDashReadOnlyRepository dashReadOnlyRepository,
            IProcessoService processoService, IPlanilhaReadOnlyRepository planilhaReadOnlyRepository, IDivisaoService divisaoService,
            ISetorService setorService, IUsuarioService usuarioService, IUsuarioReadOnlyRepository usuarioReadOnlyRepository) : base(unitOfWork)
        {
            _parametroService = parametroService;
            _dashReadOnlyRepository = dashReadOnlyRepository;
            _processoService = processoService;
            _planilhaReadOnlyRepository = planilhaReadOnlyRepository;
            _divisaoService = divisaoService;
            _setorService = setorService;
            _usuarioService = usuarioService;
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        }

        public async Task<ValidationResult> AdicionarOuAtualizar(ParametroSalvarCommand command)
        {
            if (command.QtdeSugestaoMin > command.QtdeSugestaoMax)
            {
                ValidationResult.Add("Quantidade Mínima não pode ser maoir que a Quantidade Máxima");
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

            var parametro = await _parametroService.GetByCurrentYear();
            if (parametro != null)
            {
                parametro.Editar(command.QtdeAvaliador, command.QtdeSugestaoMin, command.QtdeSugestaoMax);
                ValidationResult.Add(_parametroService.Update(parametro));
            }
            else
            {
                parametro = new Parametro(command.QtdeSugestaoMin, command.QtdeSugestaoMax, command.QtdeAvaliador);
                ValidationResult.Add(await _parametroService.CreateAsync(parametro));
            }

            if (ValidationResult.IsValid)
                await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> IniciarProcesso()
        {
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
            var setoresOuNiveis = planilhas.Select(s => s.Nivel).DistinctBy(s => s);
            foreach (var nivel in setoresOuNiveis)
            {
                var existe = setoresExistentes.FirstOrDefault(f => f.Nome.Contains(nivel));
                if (existe != null)
                {
                    setores.Add(existe);
                    continue;
                }

                var entSet = new Setor(nivel);
                setores.Add(entSet);
                await _setorService.CreateAsync(entSet);
            }

            var superiores = new List<Usuario>();
            var superiorEmails = planilhas.Where(w => w.EmailSuperior != null).Select(s => s.EmailSuperior).DistinctBy(s => s);
            foreach (var supEmail in superiorEmails)
            {
                var usuarioExiste = usuariosExistentes.FirstOrDefault(f => f.Email == supEmail);
                if (usuarioExiste != null)
                    continue;

                var supExiste = planilhas.FirstOrDefault(f => f.Email == supEmail);
                if (supExiste != null)
                {
                    var ehDiretor = !string.IsNullOrEmpty(supExiste.Direcao) && supExiste.Direcao.Contains('x');
                    var supUsuario = new Usuario(Guid.NewGuid(), supExiste.Nome, supEmail, supExiste.EmailSuperior, ehDiretor, supExiste.Nivel, "valgroup2022", supExiste.CPF);
                    var setorId = setores.First(f => f.Nome == supExiste.Nivel).Id;
                    var divisaoId = divisoes.First(f => f.Nome == supExiste.Unidade).Id;
                    var superior = superiores.FirstOrDefault(f => f.Email == supExiste.EmailSuperior);
                    supUsuario.InformarDadosExtras(setorId, divisaoId, superior != null ? superior.Id : null);
                    supUsuario.ExecutarRegraPerfil();
                    superiores.Add(supUsuario);
                    await _usuarioService.CreateAsync(supUsuario);
                }
                else
                {
                    var supSemUsuario = planilhas.FirstOrDefault(f => f.EmailSuperior == supEmail);
                    var supUsuario = new Usuario(Guid.NewGuid(), supSemUsuario.Nome, supSemUsuario.Email, null, false, null, "valgroup2022", supSemUsuario.CPF);
                    supUsuario.ExecutarRegraPerfil();
                    superiores.Add(supUsuario);
                    await _usuarioService.CreateAsync(supUsuario);
                }
            }

            var usuarios = new List<Usuario>();
            var avaliados = planilhas.Where(w => !superiorEmails.Contains(w.Email));
            foreach (var avaliado in avaliados)
            {
                var usuarioExiste = usuariosExistentes.FirstOrDefault(f => f.Email == avaliado.Email);
                if (usuarioExiste != null)
                    continue;

                var usuario = new Usuario(Guid.NewGuid(), avaliado.Nome, avaliado.Email, avaliado.EmailSuperior, false, avaliado.Nivel, "valgroup2022", avaliado.CPF);
                var setorId = setores.First(f => f.Nome == avaliado.Nivel).Id;
                var divisaoId = divisoes.First(f => f.Nome == avaliado.Unidade).Id;
                var superior = superiores.FirstOrDefault(f => f.Email == avaliado.EmailSuperior);
                usuario.InformarDadosExtras(setorId, divisaoId, superior != null ? superior.Id : null);
                usuario.ExecutarRegraPerfil();
                usuarios.Add(usuario);
                await _usuarioService.CreateAsync(usuario);
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
    }
}
