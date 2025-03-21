using System.Data;
using Dapper;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Infrastructure.Repositories;


    public class ContatoRepository : IContatoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContatoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task RemoverAsync(Guid contatoId)
        {
            var sql = "DELETE FROM Contatos WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = contatoId });
        }


    

        public async Task<Contato?> ObterPorIdAsync(Guid contatoId)
        {
            var sql = "SELECT * FROM Contatos WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Contato>(sql, new { Id = contatoId });
        }
    }
