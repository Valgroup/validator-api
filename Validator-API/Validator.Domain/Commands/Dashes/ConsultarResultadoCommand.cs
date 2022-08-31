using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Domain.Commands.Dashes
{
    public class ConsultarResultadoCommand
    {
        public Guid? DivisaoId { get; set; }
        public Guid? SetorId { get; set; }
    }
}
