using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;
using MassTransit.Mediator;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Services;

public class AtualizacaoService : IAtualizacaoService
{
    private readonly IMediator _mediator;
    private readonly ILogger<AtualizacaoService> _logger;

    public AtualizacaoService(IMediator mediator, ILogger<AtualizacaoService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task Processar(AtualizacaoContatoDto contextMessage)
    {
        _logger.LogInformation("Iniciando o processamento da mensagem UPDATE : {messageId}", contextMessage.MessageId);
        _mediator.Send(new AtualizarContatoCommand(
        contextMessage.ContatoId,
            contextMessage.Nome,
            contextMessage.Telefone,
            contextMessage.Email,
            contextMessage.Ddd
        ));
        
        return Task.CompletedTask;
    }
}