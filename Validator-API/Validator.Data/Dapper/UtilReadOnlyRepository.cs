using Dapper;
using Validator.Data.Repositories;
using Validator.Domain.Dtos;

namespace Validator.Data.Dapper
{
    public class UtilReadOnlyRepository : BaseConnection, IUtilReadOnlyRepository
    {
        public async Task<IEnumerable<SelectedItemDto>> ObterTodasDivisoes()
        {
            using var cn = CnRead;
            return await cn.QueryAsync<SelectedItemDto>(@"SELECT DISTINCT
	                                                            U.DivisaoId AS Id,
	                                                            D.Nome AS Text
                                                           FROM Usuarios U
                                                           INNER JOIN Divisao D ON D.Id = U.DivisaoId
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
                                                          ORDER BY S.Nome ");
        }
    }
}
