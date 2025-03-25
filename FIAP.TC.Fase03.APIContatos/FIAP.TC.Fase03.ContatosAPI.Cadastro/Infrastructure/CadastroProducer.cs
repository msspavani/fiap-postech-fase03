using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;

public class CadastroProducer
{
 

    private readonly ILogger<CadastroProducer> _logger;
    private readonly ISendEndpointProvider _sendEndpointProvider;


    public CadastroProducer( ILogger<CadastroProducer> logger, ISendEndpointProvider sendEndpointProvider)
    {
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task PublishMessageAsync<T>(string queueName, T message) where T : class
    {
        try
        {
            _logger.LogInformation("Publicando mensagem em : {queueName}", queueName);

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"exchange:Fiap.Fase03"));

            await sendEndpoint.Send(message, context =>
            {
                context.SetRoutingKey(queueName);
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "{hora}::: Erro ao publicar messagem {erro}",DateTime.Now, e.Message);
            throw;
        }
    }
}