using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface IUsuarioAvaliadorService : IServiceDomain<UsuarioAvaliador>
    {
        Task<UsuarioAvaliador?> Find(Expression<Func<UsuarioAvaliador, bool>> predicate);
    }
}
