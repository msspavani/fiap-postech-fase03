using System.Data;
using Dapper;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Infrastructure.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ContatoRepository> _logger;

    public ContatoRepository(IConfiguration configuration, ILogger<ContatoRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task AdicionarAsync(Contato contato)
    {
        try
        {
            const string sql = "INSERT INTO Contatos (Id, Nome, Telefone, Email, Ddd) VALUES (@Id, @Nome, @Telefone, @Email, @Ddd)";

            await using var connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection")); 
            await connection.OpenAsync(); 

            var command = new CommandDefinition(sql, contato, commandTimeout: 90, cancellationToken: CancellationToken.None);
            await connection.ExecuteAsync(command);

            _logger.LogInformation("Adicionado com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao incluir contato: {erro}", ex.Message);
            throw;
        }
    }
    
}