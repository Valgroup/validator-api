using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Application
{
    public class AppBaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ValidationResult ValidationResult { get; private set; }

        public AppBaseService(IUnitOfWork unitOfWork)
        {
            ValidationResult = new ValidationResult();
            _unitOfWork = unitOfWork;
        }

        public async Task CommitAsync()
        {
            await _unitOfWork.CommitAsync();
        }
        public void Commit()
        {
            _unitOfWork.Commit();
        }



    }
}
