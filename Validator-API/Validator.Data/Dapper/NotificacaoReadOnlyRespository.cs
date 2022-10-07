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

            var usuarios = await cn.QueryAsync<NotificacaoDto>(@"SELECT 
		                                                                US.Nome,
		                                                                US.Email,
                                                                        (SELECT TOP 1 P.DhFinalizacao FROM Parametro P) DhFinalizacao
                                                                  FROM UsuarioAvaliador UA
                                                                  INNER JOIN Usuarios U ON U.Id = UA.UsuarioId
                                                                  INNER JOIN Usuarios US ON US.SuperiorId = U.SuperiorId
                                                                  WHERE
                                                                  DATEDIFF(DAY, UA.DataHora, (SELECT TOP 1 P.DhFinalizacao FROM Parametro P)) <= 5 ");

            foreach (var item in usuarios)
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
