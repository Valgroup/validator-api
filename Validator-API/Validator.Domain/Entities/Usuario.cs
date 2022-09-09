using System.Security.Cryptography;
using System.Text;
using Validator.Domain.Core;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Usuario : EntityWithAnoBase, IAnoBase, ISoftDelete
    {
        protected Usuario() { }

        public Usuario(Guid azureId, string nome, string email, string emailSuperior, bool ehDiretor, string cargo, string senha)
        {
            AzureId = azureId;
            Nome = nome;
            Email = email;
            EmailSuperior = emailSuperior;
            EhDiretor = ehDiretor;
            Cargo = cargo;
            Senha = CryptoMD5(senha);
        }

        public Guid Id { get; private set; }
        public Guid AzureId { get; private set; }
        public Guid DivisaoId { get; private set; }
        public Guid SetorId { get; private set; }
        public Guid? SuperiorId { get; private set; }
        public EPerfilUsuario Perfil { get; private set; }
        public string Senha { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string? EmailSuperior { get; private set; }
        public string? Cargo { get; private set; }
        public bool EhDiretor { get; private set; }
        public bool Deleted { get; set; }
        public virtual Divisao Divisao { get; private set; }
        public virtual Setor Setor { get; private set; }
        public virtual Usuario? Superior { get; private set; }
        public virtual ICollection<UsuarioAvaliador> Avaliadores { get; private set; }

        public void InformarPerfil()
        {
            if (EhDiretor)
            {
                Perfil = EPerfilUsuario.Diretor;
                return;
            }

            if (Email == EmailSuperior)
            {
                Perfil = EPerfilUsuario.Ambos;
                return;
            }


        }

        public void AdicionarAvaliadores(List<Guid> ids)
        {
            Avaliadores = new List<UsuarioAvaliador>();

            foreach (var id in ids)
            {
                Avaliadores.Add(new UsuarioAvaliador(Id, id));
            }
        }

        private string CryptoMD5(string senha)
        {
            senha += "!5fa395fb-45dc-4503-b70e-c44f90048281";
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(senha));
            var sb = new StringBuilder();
            foreach (var d in data)
                sb.Append(d.ToString("x2"));

            return sb.ToString();
        }

        public bool Autenticar(string senha)
        {
            return Senha == CryptoMD5(senha);
        }
    }
}
