using System.Data;
using Dapper;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Infrastructure.Repositories;


    public class ContatoRepository : IContatoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContatoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AdicionarAsync(Contato contato)
        {
            var sql = "INSERT INTO Contatos (Id, Nome, Telefone, Email, Ddd) VALUES (@Id, @Nome, @Telefone, @Email, @Ddd)";
            await _dbConnection.ExecuteAsync(sql, contato);
        }
        
    }
