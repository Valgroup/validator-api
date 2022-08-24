using Validator.Domain.Core;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Usuario : EntityWithAnoBase, IAnoBase, ISoftDelete
    {
        public Usuario(Guid azureId, string nome, string email, string emailSuperior, bool ehDiretor)
        {
            AzureId = azureId;
            Nome = nome;
            Email = email;
            EmailSuperior = emailSuperior;
            EhDiretor = ehDiretor;
        }

        public Guid Id { get; private set; }
        public Guid AzureId { get; private set; }
        public EPerfilUsuario? Perfil { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string EmailSuperior { get; private set; }
        public bool EhDiretor { get; private set; }
        public bool Deleted { get; set; }

        public void InformarPerfil()
        {
            if (EhDiretor)
            {
                Perfil = null;
                return;
            }

            if (Email == EmailSuperior)
            {
                Perfil = EPerfilUsuario.Ambos;
                return;
            }


        }
    }
}
