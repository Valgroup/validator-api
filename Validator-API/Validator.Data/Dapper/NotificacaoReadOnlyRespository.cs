using Dapper;
using Validator.Data.Repositories;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos;
using Validator.Service.Sendgrid;

namespace Validator.Data.Dapper
{
    public class NotificacaoReadOnlyRespository : BaseConnection, INotificacaoReadOnlyRespository
    {
        private readonly ITemplateRazorService _templateRazorService;
        private readonly ISendGridService _sendGridService;
        public NotificacaoReadOnlyRespository(ITemplateRazorService templateRazorService, ISendGridService sendGridService)
        {
            _templateRazorService = templateRazorService;
            _sendGridService = sendGridService;
        }

        public async Task EnviarNotificacaoPendente(string url)
        {
            using var cn = CnRead;
            var notificacoes = new List<NotificacaoDto>();

            var usuariosSuperiores = await cn.QueryAsync<NotificacaoDto>(@"SELECT US.Nome, US.Email, (SELECT TOP 1 P.DhFinalizacao FROM Parametro P) DhFinalizacao FROM Usuarios U
                                                                           INNER JOIN Usuarios US ON US.Id = U.SuperiorId
                                                                           WHERE
                                                                           U.Id IN (SELECT UA.UsuarioId FROM UsuarioAvaliador UA WHERE UA.Status IN (0,2) AND DATEDIFF(DAY, UA.DataHora, (SELECT TOP 1 P.DhFinalizacao FROM Parametro P)) <= 5 ) ");
            if (usuariosSuperiores.Any())
            {
                notificacoes.AddRange(usuariosSuperiores);
            }

            var usuarios = await cn.QueryAsync<NotificacaoDto>(@" SELECT Id, Nome, Email, (SELECT TOP 1 P.DhFinalizacao FROM Parametro P) DhFinalizacao FROM Usuarios
                                                                  WHERE
                                                                  Perfil IN (2,4)
                                                                  AND Id NOT IN (SELECT UA.UsuarioId FROM UsuarioAvaliador UA)");

            if (usuarios.Any())
            {
                notificacoes.AddRange(usuarios);
            }

            foreach (var item in notificacoes)
            {
                var emailDto = new EmailAcessoDto
                {
                    Nome = item.Nome,
                  
                    Login = item.Email,
                    Prazo = item.DhFinalizacao.ToShortDateString(),
                    Link = url
                };

                var html = await _templateRazorService.BuilderHtmlAsString("Email/_NotificacaoPendente", emailDto);

                await _sendGridService.SendAsync(item.Nome, item.Email, html, "Avaliação Pendente");
            }
        }
    }
}
