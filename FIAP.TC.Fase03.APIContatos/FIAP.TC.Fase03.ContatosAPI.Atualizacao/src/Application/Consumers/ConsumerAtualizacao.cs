using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Consumers;

public class ConsumerAtualizacao : IConsumer<AtualizacaoContatoDto>
{
    private readonly ILogger<ConsumerAtualizacao> _logger;
    private readonly IAtualizacaoService _remocaoService;

    public ConsumerAtualizacao(IAtualizacaoService remocaoService)
    {
        _remocaoService = remocaoService;
    }

    public Task Consume(ConsumeContext<AtualizacaoContatoDto> context)
    {
        _logger.LogInformation("Message Received on DELETE worker : {messageId}", context.Message.MessageId);
        _remocaoService.Processar(context.Message);

        return Task.CompletedTask;
    }
}