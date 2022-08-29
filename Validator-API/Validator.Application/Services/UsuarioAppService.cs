using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Application.Interfaces;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Interfaces;

namespace Validator.Application.Services
{
    public class UsuarioAppService : AppBaseService, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IParametroService _parametroService;
        private readonly IUserResolver _userResolver;
        public UsuarioAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService, IParametroService parametroService, IUserResolver userResolver) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
            _parametroService = parametroService;
            _userResolver = userResolver;
        }

        public async Task<ValidationResult> DeleteAsync(Guid id)
        {
            var usuario = _usuarioService.GetById(id);
            if (usuario == null)
            {
                ValidationResult.Add("Registro não encontrado");
                return ValidationResult;
            }

            if (usuario.Perfil == Domain.Core.Enums.EPerfilUsuario.Administrador)
            {
                ValidationResult.Add("Usuário não pode ser excluído");
                return ValidationResult;
            }

            _usuarioService.Delete(usuario);

            if (ValidationResult.IsValid)
                await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> EscolherAvaliadores(List<Guid> ids)
        {
            var parametro = await _parametroService.GetByCurrentYear();
            if (ids.Count > parametro.QtdeAvaliador)
            {
                ValidationResult.Add($"Só é permitido escolher {parametro.QtdeAvaliador} avaliadores");
                return ValidationResult;
            }

            var userAuth = await _userResolver.GetAuthenticateAsync();
            var usuario = _usuarioService.GetById(userAuth.Id);

            usuario.AdicionarAvaliadores(ids);

            _usuarioService.Update(usuario);

            await CommitAsync();

            return ValidationResult;
        }
    }
}
