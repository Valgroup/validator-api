using Validator.Application.Interfaces;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Application.Services
{
    public class PlanilhaAppService : AppBaseService, IPlanilhaAppService
    {
        public PlanilhaAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ValidationResult> Updload(object objPlanilha)
        {



            return ValidationResult;
        }
    }
}
