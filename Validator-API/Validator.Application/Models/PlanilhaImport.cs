using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Application.Validations;
using Validator.Domain.Entities;

namespace Validator.Application.Models
{
    public class PlanilhaImport : PlanilhaImportBase, IPlanilha
    {
        public PlanilhaImport(int line) : base(line)
        {
        }

        [Required]
        [CPF]
        public string CPF { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Required]
        public string CentroCusto { get; set; }

        [Required]
        public DateTime DataAdmissao { get; set; }

        //public bool Deleted { get; set; }

        [Required]
        public string Divisoes { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        public string EmailSuperior { get; set; }

        [Required]
        public string Nivel { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string NumeroCentroCusto { get; set; }

        public string SuperiorImediato { get; set; }

        [Required]
        public string Unidade { get; set; }

    }
}
