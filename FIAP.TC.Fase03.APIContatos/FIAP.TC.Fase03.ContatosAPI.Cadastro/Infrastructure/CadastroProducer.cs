using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;

public class CadastroProducer
{
 
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CadastroProducer> _logger;


    public CadastroProducer(IPublishEndpoint publishEndpoint, ILogger<CadastroProducer> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishMessageAsync<T>(string queueName, T message) where T : class
    {
        try
        {
            _logger.LogInformation("Publiando mensagem em : {queueName}", queueName);

            await _publishEndpoint.Publish(message, context =>
            {
                context.SetRoutingKey(queueName);
                context.Headers.Set("MT-MessageType", "Fiap.Fase03");
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao publicar messagem {erro}", e.Message);
            throw;
        }
    }
}