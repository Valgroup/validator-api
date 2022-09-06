using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Domain.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IRepository<Processo> _repository;
        private readonly IUtilReadOnlyRepository _utilReadOnlyRepository;

        public ProcessoService(IRepository<Processo> repository, IUtilReadOnlyRepository utilReadOnlyRepository)
        {
            _repository = repository;
            _utilReadOnlyRepository = utilReadOnlyRepository;
        }

        public async Task Atualizar(bool? temPendencia = null)
        {
            if (!temPendencia.HasValue)
                temPendencia = await _utilReadOnlyRepository.TemPendencias();

            var processo = await _repository.GetByCurrentYear();
            if (processo == null)
            {
                processo = new Processo(Core.Enums.ESituacaoProcesso.ComPendencia);
                processo.InformarSituacao(temPendencia.Value);
                await _repository.CreateAsync(processo);

            }
            else
            {
                processo.InformarSituacao(temPendencia.Value);
                _repository.Update(processo);
            }
        }
    }
}
