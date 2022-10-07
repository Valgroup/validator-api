using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Domain.Dtos
{
    public class NotificacaoDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DhFinalizacao { get; set; }
    }
}
