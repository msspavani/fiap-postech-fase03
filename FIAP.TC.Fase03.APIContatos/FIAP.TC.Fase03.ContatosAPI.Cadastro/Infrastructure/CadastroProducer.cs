using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;

public class CadastroProducer
{
    private readonly ILogger<CadastroProducer> _logger;
    private readonly ISendEndpointProvider _sendEndpointProvider;


    public CadastroProducer(ISendEndpointProvider sendEndpointProvider, ILogger<CadastroProducer> logger)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _logger = logger;
    }

    public async Task PublishMessageAsync<T>(string routingKey, T message) where T : class
    {
        try
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("exchange:Fiap.Fase03"));

            await endpoint.Send(message, context =>
            {
                context.Headers.Set("RoutingKey", routingKey);
            });

            _logger.LogInformation("Mensagem publicada com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao publicar mensagem: {Message}", ex.Message);
            throw;
        }
    }
}