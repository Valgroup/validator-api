using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Core.Enums;

namespace Validator.Domain.Dtos.Usuarios
{
    public class UsuarioAuthDto
    {
        public Guid Id { get; set; }
        public Guid AnoBaseId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public EPerfilUsuario Perfil { get; set; }

    }
}
