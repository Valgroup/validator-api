using Dapper;
using Validator.Data.Repositories;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Data.Dapper
{
    public class UtilReadOnlyRepository : BaseConnection, IUtilReadOnlyRepository
    {
        private readonly IUserResolver _userResolver;

        public UtilReadOnlyRepository(IUserResolver userResolver)
        {
            _userResolver = userResolver;
        }

        public async Task<IEnumerable<SelectedItemDto>> ObterTodasDivisoes()
        {
            using var cn = CnRead;
            return await cn.QueryAsync<SelectedItemDto>(@"SELECT DISTINCT
	                                                            U.DivisaoId AS Id,
	                                                            D.Nome AS Text
                                                           FROM Usuarios U
                                                           INNER JOIN Divisao D ON D.Id = U.DivisaoId
                                                           INNER JOIN UsuarioAvaliador UA ON UA.UsuarioId = U.Id OR UA.AvaliadorId = U.Id
                                                           ORDER BY D.Nome ");
        }

        public async Task<IEnumerable<SelectedItemDto>> ObterTodosSetores()
        {
            using var cn = CnRead;
            return await cn.QueryAsync<SelectedItemDto>(@"SELECT DISTINCT
	                                                            U.SetorId AS Id,
	                                                            S.Nome AS Text
                                                          FROM Usuarios U
                                                          INNER JOIN Setor S ON S.Id = U.SetorId
                                                          INNER JOIN UsuarioAvaliador UA ON UA.UsuarioId = U.Id OR UA.AvaliadorId = U.Id
                                                          ORDER BY S.Nome ");
        }

        public async Task<bool> TemPendencias()
        {
            using var cn = CnRead;
            var pendencias = await cn.QueryAsync<Guid>(@" SELECT TOP 1 Id FROM Planilhas 
                                                          WHERE
                                                          AnoBaseId = @AnoBaseId
                                                          AND EhValido = 0 ", new { AnoBaseId = await _userResolver.GetYearIdAsync() }); ;

            return pendencias.Any();


        }
    }
}
