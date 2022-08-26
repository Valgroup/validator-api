using Validator.Domain.Core.Models;

namespace Validator.Domain.Core.Interfaces
{
    public interface IUserResolver
    {
        Task<Guid> GetYearIdAsync();
        Task<UsarioJwt> GetAuthenticateAsync();
    }
}
