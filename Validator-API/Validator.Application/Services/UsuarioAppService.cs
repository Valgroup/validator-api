using Validator.Application.Interfaces;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Application.Services
{
    public class UsuarioAppService : AppBaseService, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IParametroService _parametroService;
        private readonly IUserResolver _userResolver;
        private readonly IUsuarioAvaliadorService _usuarioAvaliadorService;
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IProgressoService _progressoService;
        public UsuarioAppService(IUnitOfWork unitOfWork, IUsuarioService usuarioService,
            IParametroService parametroService,
            IUserResolver userResolver,
            IUsuarioAvaliadorService usuarioAvaliadorService, IUsuarioReadOnlyRepository usuarioReadOnlyRepository, IProgressoService progressoService) : base(unitOfWork)
        {
            _usuarioService = usuarioService;
            _parametroService = parametroService;
            _userResolver = userResolver;
            _usuarioAvaliadorService = usuarioAvaliadorService;
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _progressoService = progressoService;
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

            if (ids.Count < parametro.QtdeSugestaoMin)
            {
                ValidationResult.Add($"Só é permitido escolher a quantidade miníma de {parametro.QtdeSugestaoMin} avaliadores");
                return ValidationResult;
            }

            if (ids.Count > parametro.QtdeSugestaoMax)
            {
                ValidationResult.Add($"Só é permitido escolher a quantidade máxima de {parametro.QtdeSugestaoMin} avaliadores");
                return ValidationResult;
            }

            var userAuth = await _userResolver.GetAuthenticateAsync();

            var avaliadoresExitentes = await _usuarioReadOnlyRepository.ObterAvaliadores(new AvaliadoresConsultaCommand { Take = 10 });
                       
            foreach (var avaliadorId in ids)
            {
                var jaExiste = avaliadoresExitentes.Records.FirstOrDefault(f => f.AvaliadorId == avaliadorId);
                if (jaExiste != null)
                {
                    ValidationResult.Add($"O Avaliador {jaExiste.Nome} já foi escolhido para te Avaliar");
                    return ValidationResult;
                }

                var usuarioAvaliador = new UsuarioAvaliador(userAuth.Id, avaliadorId);
                await _usuarioAvaliadorService.CreateAsync(usuarioAvaliador);
            }

            var progresso = new Progresso(userAuth.Id, Domain.Core.Enums.EStatuAvaliador.Enviada);
            await _progressoService.CreateAsync(progresso);

            await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> SubstituirAvaliador(SubstituirAvaliadorCommand command)
        {
            //var user = await _userResolver.GetAuthenticateAsync();
            var usuarioAvaliador = await _usuarioAvaliadorService.Find(f => f.UsuarioId == command.AvaliadoId && f.AvaliadorId == command.AvaliadorAntigoId);
            if (usuarioAvaliador == null)
            {
                ValidationResult.Add("Sugestão de Avaliação não encontrada");
                return ValidationResult;
            }

            if (usuarioAvaliador.Status == Domain.Core.Enums.EStatuAvaliador.Enviada)
            {
                ValidationResult.Add("Não pode substituir esse Avaliador pois já foi enviado para Aprovação");
                return ValidationResult;
            }

            usuarioAvaliador.AlterarAvaliador(command.AvaliadorNovoId);

            _usuarioAvaliadorService.Update(usuarioAvaliador);

            await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> AprovarSubordinado(List<Guid> usuarioIds)
        {
            var parametros = await _parametroService.GetByCurrentYear();
            int qtdAvaliadores = parametros.QtdeAvaliador;
            foreach (var usuarioId in usuarioIds)
            {
                var sugestoes = await _usuarioAvaliadorService.FindAll(f => f.UsuarioId == usuarioId, false);
                if (sugestoes.Count() > qtdAvaliadores)
                {
                    var usuario = await _usuarioReadOnlyRepository.ObterDetalhes(usuarioId);
                    ValidationResult.Add($"Quantidade avaliadores para {usuario.Nome} ultrapassou o limite de {qtdAvaliadores} Avalidadores");
                    return ValidationResult;
                }

                foreach (var item in sugestoes)
                {
                    if (!item.Usuario.Ativo)
                    {
                        ValidationResult.Add($"Não foi possível aprovar as avaliações de {item.Usuario.Nome} pois o mesmo está desativado");
                        return ValidationResult;
                    }

                    if (!item.Avaliador.Ativo)
                    {
                        ValidationResult.Add($"Não foi possível aprovar as avaliações de {item.Usuario.Nome} pois o avaliador {item.Avaliador.Nome} está desativado");
                        return ValidationResult;
                    }

                    item.Aprovar();
                    _usuarioAvaliadorService.Update(item);
                }

                var progresso = new Progresso(usuarioId, Domain.Core.Enums.EStatuAvaliador.Confirmada);
                await _progressoService.CreateAsync(progresso);
            }
            
            await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> AtivarOuDesativar(Guid usuarioId, bool valor)
        {
            var usuario = await _usuarioService.GetByIdAsync(usuarioId);
            if (usuario == null)
            {
                ValidationResult.Add("Usuário não encontrado");
                return ValidationResult;
                   
            }

            if (usuario.Perfil == Domain.Core.Enums.EPerfilUsuario.Administrador)
            {
                ValidationResult.Add("Usuário Administradores não pode ser desativados");
                return ValidationResult;
            }

            //COMO SE FOSSE O DELETE
            usuario.AtivartOuDesativar(valor);

            _usuarioService.Update(usuario);

            await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> ExcluirAvaliador(ExcluirSugestaoAvaliadoCommand command)
        {
            var usuarioAvaliador = await _usuarioAvaliadorService.Find(f => f.UsuarioId == command.AvaliadoId && f.AvaliadorId == command.AvaliadorId);
            if (usuarioAvaliador == null)
            {
                ValidationResult.Add("Sugestão não encontrada");
                return ValidationResult;
            }

            _usuarioAvaliadorService.Delete(usuarioAvaliador);

            await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> AdicionarAvaliador(AdicionarAvaliadorCommand command)
        {
            var avaliadores = await _usuarioAvaliadorService.FindAll(f => f.UsuarioId == command.AvaliadorId, false);
            var avaliador = avaliadores.FirstOrDefault(a => a.AvaliadorId == command.AvaliadorId);
            if (avaliador != null)
            {
                var usuario = await _usuarioReadOnlyRepository.ObterDetalhes(command.AvaliadorId);
                ValidationResult.Add($"O Avaliador {usuario.Nome} já foi escolhido para Avaliar esse Avaliado");
                return ValidationResult;
            }

            var usuarioAvaliador = new UsuarioAvaliador(command.AvaliadoId, command.AvaliadorId);

            await _usuarioAvaliadorService.CreateAsync(usuarioAvaliador);

            await CommitAsync();

            return ValidationResult;

        }
    }
}
