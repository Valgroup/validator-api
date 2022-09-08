﻿using Dapper;
using System.Text;
using Validator.Data.Repositories;
using Validator.Domain.Commands;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Usuarios;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Data.Dapper
{
    public class UsuarioReadOnlyRepository : BaseConnection, IUsuarioReadOnlyRepository
    {
        private readonly IUserResolver _userResolver;

        public UsuarioReadOnlyRepository(IUserResolver userResolver)
        {
            _userResolver = userResolver;
        }

        public async Task<IPagedResult<AvaliadorDto>> ObterAvaliadores(AvaliadoresConsultaCommand command)
        {
            using var cn = CnRead;

            var qrySb = new StringBuilder();

            qrySb.Append(@"SELECT 
		                        U.Nome,
                                UA.AvaliadorId,
		                        S.Nome AS Setor,
		                        D.Nome AS Divisao,
		                        U.Cargo,
                                COUNT(1) OVER() AS Total
                          FROM UsuarioAvaliador UA
                          INNER JOIN Usuarios U ON U.Id = UA.AvaliadorId
                          INNER JOIN Setor S ON S.Id = U.SetorId
                          INNER JOIN Divisao D ON D.Id = U.DivisaoId
                          WHERE
                          UA.UsuarioId = @UsuarioId  ");

            qrySb.Append("ORDER BY U.Nome ");
            qrySb.Append("OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ");

            var user = await _userResolver.GetAuthenticateAsync();
            var usuarios = await cn.QueryAsync<AvaliadorDto>(qrySb.ToString(), new
            {
                AnoBaseId = user.AnoBaseId,
                Skip = command.Skip,
                Take = command.Take,
                UsuarioId = user.Id
            });

            int total = usuarios.FirstOrDefault() != null ? usuarios.FirstOrDefault().Total : 0;

            return new PagedResult<AvaliadorDto>
            {
                Records = usuarios,
                RecordsTotal = total,
                RecordsFiltered = command.Take
            };

        }

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

        public async Task<IPagedResult<UsuarioSubordinadoDto>> ObterSubordinados(PaginationBaseCommand command)
        {
            using var cn = CnRead;

            var qrySb = new StringBuilder();

            qrySb.Append(@"SELECT 
	                            U.Id,
	                            U.Email,
	                            S.Nome AS Setor,
	                            D.Nome AS Unidade,
	                            CASE WHEN UA.Status = 0 THEN 'Pendente'
		                             WHEN UA.Status = 1 THEN 'Aprovado'
	                            END AS Status,
	                            COUNT(1) OVER() AS Total 
                            FROM Usuarios U
                            INNER JOIN AnoBases A ON A.AnoBaseId = U.AnoBaseId AND A.Deleted = 0
                            INNER JOIN Setor S ON S.Id = U.SetorId
                            INNER JOIN Divisao D ON D.Id = U.DivisaoId
                            INNER JOIN UsuarioAvaliador UA ON UA.UsuarioId = U.Id
                            WHERE
                            A.AnoBaseId = @AnoBaseId
                            AND U.SuperiorId = @SuperiorId ");

            qrySb.Append("ORDER BY U.Nome ");
            qrySb.Append("OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ");

            var user = await _userResolver.GetAuthenticateAsync();
            var usuarios = await cn.QueryAsync<UsuarioSubordinadoDto>(qrySb.ToString(), new
            {
                AnoBaseId = user.AnoBaseId,
                SuperiorId = user.Id,
                Skip = command.Skip,
                Take = command.Take
            });

            int total = usuarios.FirstOrDefault() != null ? usuarios.FirstOrDefault().Total : 0;

            return new PagedResult<UsuarioSubordinadoDto>
            {
                Records = usuarios,
                RecordsTotal = total,
                RecordsFiltered = command.Take
            };
        }

        public async Task<IPagedResult<AvaliadorDto>> ObterSugestaoAvaliadores(SugestaoAvaliadoresConsultaCommand command)
        {
            var usuarios = await Todos(new UsuarioAdmConsultaCommand { DivisaoId = command.DivisaoId, Page = command.Page, QueryNome = command.QueryNome, Take = command.Take });

            return new PagedResult<AvaliadorDto>
            {
                RecordsFiltered = usuarios.RecordsFiltered,
                RecordsTotal = usuarios.RecordsTotal,
                Records = usuarios.Records.Select(s => new AvaliadorDto { AvaliadorId = s.Id, Cargo = s.Cargo, Divisao = s.Unidade, Nome = s.Nome, Setor = s.Setor, Total = s.Total })
            };

        }

        public async Task<IPagedResult<UsuarioDto>> Todos(UsuarioAdmConsultaCommand command)
        {
            using var cn = CnRead;
            var usuario = await _userResolver.GetAuthenticateAsync();

            var qrySb = new StringBuilder();

            qrySb.Append(@"SELECT 
	                            U.Id,
                                U.Nome,
	                            U.Email,
	                            S.Nome AS Setor,
	                            D.Nome AS Unidade,
	                            U.Cargo,
	                            SUP.Nome AS Superior,
	                            COUNT(1) OVER() AS Total 
                            FROM Usuarios U
                            INNER JOIN AnoBases A ON A.AnoBaseId = U.AnoBaseId AND A.Deleted = 0
                            INNER JOIN Setor S ON S.Id = U.SetorId
                            INNER JOIN Divisao D ON D.Id = U.DivisaoId
                            LEFT JOIN Usuarios SUP ON SUP.Id = U.SuperiorId
                            WHERE
                            A.AnoBaseId = @AnoBaseId AND U.Perfil != 1 AND U.Deleted = 0 AND U.Id != @UsuarioId ");

            if (usuario.SuperiorId.HasValue)
                qrySb.Append(" AND U.Id != @SuperiorId ");

            if (command.DivisaoId.HasValue)
                qrySb.Append(" AND D.Id = @DivisaoId ");

            var divisoes = new List<string> { "SP1", "MG2" };
            if (divisoes.Contains(usuario.DivisaoNome) && !command.DivisaoId.HasValue)
            {
                qrySb.Append(" AND D.Nome IN ('SP1', 'MG2') ");
            }
            else if (!divisoes.Contains(usuario.DivisaoNome) && !command.DivisaoId.HasValue)
            {
                qrySb.Append(" AND D.Nome NOT IN ('SP1', 'MG2') ");
            }

            if (!string.IsNullOrEmpty(command.QueryNome))
                qrySb.Append(" AND (U.Nome LIKE @WhereLike OR U.Email LIKE @WhereLike) ");

            qrySb.Append("ORDER BY U.Nome ");
            qrySb.Append("OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ");

            var yearId = await _userResolver.GetYearIdAsync();
            var usuarios = await cn.QueryAsync<UsuarioDto>(qrySb.ToString(), new
            {
                AnoBaseId = yearId,
                DivisaoId = command.DivisaoId,
                WhereLike = $"'%{command.QueryNome}%'",
                Skip = command.Skip,
                Take = command.Take,
                UsuarioId = usuario.Id,
                SuperiorId = usuario.SuperiorId
            });

            int total = usuarios.FirstOrDefault() != null ? usuarios.FirstOrDefault().Total : 0;

            return new PagedResult<UsuarioDto>
            {
                Records = usuarios,
                RecordsTotal = total,
                RecordsFiltered = command.Take
            };


        }
    }
}
