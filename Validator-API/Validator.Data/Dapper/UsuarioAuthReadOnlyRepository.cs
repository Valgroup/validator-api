using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return await cn.QueryFirstOrDefaultAsync<UsuarioAuthDto>(@"SELECT 
	                                                                        U.Id,
	                                                                        U.Nome,
	                                                                        U.Email,
	                                                                        U.Perfil,
	                                                                        U.AnoBaseId
                                                                        FROM Usuarios U
                                                                        INNER JOIN AnoBases A ON A.AnoBaseId = U.AnoBaseId AND A.Deleted = 0
                                                                        WHERE
                                                                        Id = @Id ", new { Id = id });
        }
    }
}
