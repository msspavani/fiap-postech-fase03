using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Consumers;

public class ConsumerAtualizacao : IConsumer<AtualizacaoContatoDto>
{
    private readonly ILogger<ConsumerAtualizacao> _logger;
    private readonly IAtualizacaoService _atualizacaoService;

    public ConsumerAtualizacao(IAtualizacaoService atualizacaoService, ILogger<ConsumerAtualizacao> logger)
    {
        _atualizacaoService = atualizacaoService;
        _logger = logger;
    }

    public Task Consume(ConsumeContext<AtualizacaoContatoDto> context)
    {
        _logger.LogInformation("Mensagem recebida no Consumer: {MessageJson}", JsonSerializer.Serialize(context.Message));

        _logger.LogInformation("Message Received on UPDATE worker : {messageId}", context.Message.MessageId);
        _atualizacaoService.Processar(context.Message);

        return Task.CompletedTask;
    }
}