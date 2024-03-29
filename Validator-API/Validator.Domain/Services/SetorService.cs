﻿using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class SetorService : ServiceDomain<Setor>, ISetorService
    {
        private readonly IRepository<Setor> _repository;

        public SetorService(IRepository<Setor> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Setor>> FindAllByYear()
        {
            return await _repository.FindAllByYear();
        }
    }
}
