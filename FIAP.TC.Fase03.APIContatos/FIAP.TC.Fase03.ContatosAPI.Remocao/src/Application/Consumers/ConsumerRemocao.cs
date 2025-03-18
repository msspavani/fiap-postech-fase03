using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Consumers;

public class ConsumerRemocao : IConsumer<RemocaoContatoDto>
{
    
    private readonly ILogger<ConsumerRemocao> _logger;
    private readonly IRemocaoService _remocaoService;

    public ConsumerRemocao(ILogger<ConsumerRemocao> logger, IRemocaoService remocaoService)
    {
        _logger = logger;
        _remocaoService = remocaoService;
    }

    public Task Consume(ConsumeContext<RemocaoContatoDto> context)
    {
        _logger.LogInformation("Message Received on DELETE worker : {messageId}", context.Message.MessageId);
        _remocaoService.Processar(context.Message);

        return Task.CompletedTask;
    }
}