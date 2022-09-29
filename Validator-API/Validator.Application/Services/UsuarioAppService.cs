﻿using Validator.Application.Interfaces;
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
            if (ids.Count > parametro.QtdeAvaliador)
            {
                ValidationResult.Add($"Só é permitido escolher {parametro.QtdeAvaliador} avaliadores");
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
            foreach (var usuarioId in usuarioIds)
            {
                var sugestoes = await _usuarioAvaliadorService.FindAll(f => f.UsuarioId == usuarioId);
                foreach (var item in sugestoes)
                {
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
    }
}
