using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;

public class CadastroProducer
{
    private readonly ILogger<CadastroProducer> _logger;
    private readonly IPublishEndpoint _sendEndpointProvider;


    public CadastroProducer(IPublishEndpoint sendEndpointProvider, ILogger<CadastroProducer> logger)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _logger = logger;
    }

    public async Task PublishMessageAsync<T>(string routingKey, T message) where T : class
    {
        try
        {
            if (string.IsNullOrWhiteSpace(routingKey))
                throw new ArgumentException("RoutingKey não pode ser vazia", nameof(routingKey));

            
            _ = typeof(T).Name switch
            {
                nameof(MensagemEnvelopeCreate) => "Fiap.Fase03.Create",
                nameof(MensagemEnvelopeRemove) => "Fiap.Fase03.Remove",
                nameof(MensagemEnvelopeUpdate) => "Fiap.Fase03.Update",
                _ => throw new ArgumentException($"Tipo de mensagem não suportado: {typeof(T).Name}")
            };

            
            await _sendEndpointProvider.Publish(message, context =>
            {
                context.Headers.Set("RoutingKey", routingKey);
            });

            _logger.LogInformation("Mensagem publicada com sucesso para tipo {MessageType} com routingKey {RoutingKey}", typeof(T).Name, routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao publicar mensagem: {Message}", ex.Message);
            throw;
        }
    }

}