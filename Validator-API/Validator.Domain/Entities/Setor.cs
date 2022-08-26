using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Setor : EntityWithAnoBase, IAnoBase
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
    }

}