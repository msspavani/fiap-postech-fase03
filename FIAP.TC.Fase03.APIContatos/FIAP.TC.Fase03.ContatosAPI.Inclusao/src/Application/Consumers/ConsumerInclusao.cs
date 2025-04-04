using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Application;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Consumers;

public class ConsumerInclusao : IConsumer<MensagemEnvelope>
{
    private readonly ILogger<ConsumerInclusao> _logger;
    private readonly IServiceInclusao _serviceInclusao;

    public ConsumerInclusao(ILogger<ConsumerInclusao> logger, IServiceInclusao serviceInclusao)
    {
        _logger = logger;
        this._serviceInclusao = serviceInclusao;
    }

    public Task Consume(ConsumeContext<MensagemEnvelope> context)
    {
        
        var rawPayload = context.Message.Payload;
        
        _logger.LogInformation("Message Received on INSERT worker ");
        
        if(rawPayload is MensagemInclusaoDTO)
            _serviceInclusao.Processar((MensagemInclusaoDTO)rawPayload);

        return Task.CompletedTask;
    }
}