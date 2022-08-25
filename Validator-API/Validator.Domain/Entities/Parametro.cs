using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Domain.Entities
{
    public class Parametro
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public int QtdeSugestaoMin { get; private set; }
        public int QtdeSugestaoMax { get; private set; }
        public int QtdeAvaliador { get; private set; }
        public virtual Usuario Usuario { get; private set; }
    }
}
