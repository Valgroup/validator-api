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
        private readonly IDashReadOnlyRepository _dashReadOnlyRepository;
        public DashAppService(IUnitOfWork unitOfWork, IParametroService parametroService, IDashReadOnlyRepository dashReadOnlyRepository) : base(unitOfWork)
        {
            _parametroService = parametroService;
            _dashReadOnlyRepository = dashReadOnlyRepository;
        }

        public async Task<ValidationResult> AdicionarOuAtualizar(ParametroSalvarCommand command)
        {
            if (command.QtdeSugestaoMin > command.QtdeSugestaoMax)
            {
                ValidationResult.Add("Quantidade Mínima não pode ser maoir que a Quantidade Máxima");
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
