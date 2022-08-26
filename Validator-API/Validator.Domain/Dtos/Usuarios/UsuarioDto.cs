using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Domain.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public Guid Id  { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Setor { get; set; }
        public string Unidade { get; set; }
        public string Divisoes { get; set; }
        public string Cargo { get; set; }
        public string SuperiorImediato { get; set; }
    }
}
