using Dapper;
using Validator.Data.Repositories;
using Validator.Domain.Dtos.Usuarios;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Data.Dapper
{
    public class UsuarioAuthReadOnlyRepository : BaseConnection, IUsuarioAuthReadOnlyRepository
    {
        public async Task<UsuarioAuthDto> ObterPorId(Guid id)
        {
            using var cn = CnRead;
            var usuario = await cn.QueryFirstOrDefaultAsync<UsuarioAuthDto>(@"SELECT 
	                                                                        U.Id,
	                                                                        U.Nome,
	                                                                        U.Email,
	                                                                        U.Perfil,
	                                                                        U.AnoBaseId,
                                                                            U.DivisaoId,
                                                                            U.SuperiorId
                                                                        FROM Usuarios U
                                                                        INNER JOIN AnoBases A ON A.AnoBaseId = U.AnoBaseId AND A.Deleted = 0
                                                                        WHERE
                                                                        Id = @Id ", new { Id = id });

            var divisaoNome = await cn.QueryFirstOrDefaultAsync<string>("SELECT Nome FROM Divisao WHERE Id = @Id ", new { Id = usuario.DivisaoId });

            usuario.DivisaoNome = divisaoNome;

            return usuario;
        }
    }
}
