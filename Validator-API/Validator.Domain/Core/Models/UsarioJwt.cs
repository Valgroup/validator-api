﻿using Validator.Domain.Core.Enums;

namespace Validator.Domain.Core.Models
{
    public class UsarioJwt
    {
        public Guid Id { get; set; }
        public Guid AnoBaseId { get; set; }
        public Guid? DivisaoId { get; set; }
        public Guid? SuperiorId { get; set; }
        public string DivisaoNome { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NomeSuperior { get; set; }
        public string EmailSuperior { get; set; }
        public EPerfilUsuario Perfil { get; set; }
        public string PerfilNome { get { return Perfil.ToString(); } }
        public PermissaoJwt Permissao { get; set; }
    }

    public class PermissaoJwt
    {
        public PermissaoJwt()
        {
            LimparBase = false;
            Documento = true;
            ConsutarUsuarios = true;
            LiberarProcesso = true;
            Download = false;
            SugestaoEnviada = false;
        }

        public bool LimparBase { get; set; }
        public bool Documento { get; set; }
        public bool ConsutarUsuarios { get; set; }
        public bool LiberarProcesso { get; set; }
        public bool HabilitarParametros { get; set; }
        public bool Download { get; set; }
        public bool SugestaoEnviada { get; set; }
        public int QtdTotal { get; set; }
        public int QtdPendentes { get; set; }
    }
}
