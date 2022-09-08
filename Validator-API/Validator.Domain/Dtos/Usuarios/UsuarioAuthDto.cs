using Validator.Domain.Core.Enums;

namespace Validator.Domain.Dtos.Usuarios
{
    public class UsuarioAuthDto
    {
        public Guid Id { get; set; }
        public Guid AnoBaseId { get; set; }
        public Guid? SuperiorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Guid DivisaoId { get; set; }
        public string DivisaoNome { get; set; }
        public EPerfilUsuario Perfil { get; set; }
    }
}
