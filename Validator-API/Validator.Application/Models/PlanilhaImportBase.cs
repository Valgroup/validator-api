using System;
using System.ComponentModel.DataAnnotations;

namespace Validator.Application.Models
{
    public abstract class PlanilhaImportBase : IPlanilhaImport
    {
        private ValidationContext context;
        private IList<System.ComponentModel.DataAnnotations.ValidationResult> validationResults;
        public PlanilhaImportBase(int line)
        {
            Line = line;
            context = new ValidationContext(this, serviceProvider: null, items: null);
            validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        }

        public int Line { get; set; }

        public IList<System.ComponentModel.DataAnnotations.ValidationResult> ValidationResults { get { return validationResults; } }

        public bool IsValid { get => System.ComponentModel.DataAnnotations.Validator.TryValidateObject(this, context, validationResults, true); }
    }
}
