using Validator.Application.Interfaces;
using Validator.Data.Dapper;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos.Dashes;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Application.Services
{
    public class DashAppService : AppBaseService, IDashAppService
    {
        private readonly IParametroService _parametroService;
        private readonly IProcessoService _processoService;
        private readonly IDashReadOnlyRepository _dashReadOnlyRepository;
        public DashAppService(IUnitOfWork unitOfWork, IParametroService parametroService, IDashReadOnlyRepository dashReadOnlyRepository, IProcessoService processoService) : base(unitOfWork)
        {
            _parametroService = parametroService;
            _dashReadOnlyRepository = dashReadOnlyRepository;
            _processoService = processoService;
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
