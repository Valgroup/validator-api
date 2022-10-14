using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Models;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.API.Resolvers
{
    public class WebApiResolver : IUserResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioAuthReadOnlyRepository _usuarioReadOnlyRepository;

        public WebApiResolver(IHttpContextAccessor httpContextAccessor, IUsuarioAuthReadOnlyRepository usuarioReadOnlyRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        }

        public async Task<UsarioJwt> GetAuthenticateAsync()
        {
            var headerValues = new StringValues();
            var headers = _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Token", out headerValues);
            if (headers == true)
            {
                var accessTokenCry = headerValues.ToString();
                var id = Guid.Parse(CryptoHelper.Decrypt(accessTokenCry));
                var usuario = await _usuarioReadOnlyRepository.ObterPorId(id);
                if (usuario != null)
                    return new UsarioJwt
                    {
                        Email = usuario.Email,
                        Id = id,
                        Nome = usuario.Nome,
                        Perfil = usuario.Perfil,
                        AnoBaseId = usuario.AnoBaseId,
                        DivisaoId = usuario.DivisaoId,
                        DivisaoNome = usuario.DivisaoNome,
                        SuperiorId = usuario.SuperiorId
                    };
            }

            return null;
        }

        public async Task<Guid> GetYearIdAsync()
        {
            var user = await GetAuthenticateAsync();
            if (user != null)
                return user.AnoBaseId;

            return Guid.Empty;
        }

        public async Task<bool> IsAdministrator()
        {
            var user = await GetAuthenticateAsync();
            if (user != null)
                return user.Perfil == Domain.Core.Enums.EPerfilUsuario.Administrador;

            return false;
        }
    }
}
