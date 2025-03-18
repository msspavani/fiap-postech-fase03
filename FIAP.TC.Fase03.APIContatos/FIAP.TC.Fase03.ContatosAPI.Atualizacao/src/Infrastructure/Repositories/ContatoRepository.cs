using System.Data;
using Dapper;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;


namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Infrastructure.Repositories;


    public class ContatoRepository : IContatoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContatoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AtualizarAsync(Contato contato)
        {
            var sql = "UPDATE Contatos SET Nome = @Nome, Telefone = @Telefone, Email = @Email, Ddd = @Ddd WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, contato);
        }

    

        public async Task<Contato?> ObterPorIdAsync(Guid contatoId)
        {
            var sql = "SELECT * FROM Contatos WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Contato>(sql, new { Id = contatoId });
        }
    }
