
using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FIAP.TC.Fase03.ContatosAPI.Consulta;

public class ContatosFunction
{
    private readonly string _connectionString;
    private readonly ILogger<ContatosFunction> _logger;

    public ContatosFunction(IConfiguration config, ILogger<ContatosFunction> logger)
    {
        _connectionString = config.GetConnectionString("SqlServer");
        _logger = logger;
    }

    [Function("ObterContatoPorId")]
    public async Task<HttpResponseData> ObterContatoPorId(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contatos/{id}")] HttpRequestData req,
        string id)
    {
        _logger.LogInformation($"Recebida requisição para obter contato por ID: {id}");

        if (!Guid.TryParse(id, out var contatoId))
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("ID inválido.");
            return badRequestResponse;
        }
        
        using var connection = new SqlConnection(_connectionString);
        var contato = await connection.QueryFirstOrDefaultAsync<Contato>(
            "SELECT Id, Nome, Telefone, Ddd FROM Contatos WHERE Id = @Id",
            new { Id = contatoId });

        if (contato == null)
        {
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteStringAsync("Contato não encontrado.");
            return notFoundResponse;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(contato);
        return response;
    }

    [Function("ObterContatosPorDdd")]
    public async Task<HttpResponseData> ObterContatosPorDdd(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contatos/ddd/{ddd}")] HttpRequestData req,
        string ddd)
    {
        _logger.LogInformation($"Recebida requisição para obter contatos pelo DDD: {ddd}");

        using var connection = new SqlConnection(_connectionString);
        var contatos = await connection.QueryAsync<Contato>(
            "SELECT Id, Nome, Telefone, Ddd FROM Contatos WHERE Ddd = @Ddd",
            new { Ddd = ddd });

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(contatos);
        return response;
    }

    private class Contato
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Ddd { get; set; }
    }
}

public class ConsultaFunction
{
    
}