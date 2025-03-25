using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Shared.Domain.Dtos;
using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Services;

public class InclusaoService : IServiceInclusao
{
    private readonly IMediator _mediator;
    private ILogger<InclusaoService> _logger;

    public InclusaoService(IMediator mediator, ILogger<InclusaoService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task Processar(MensagemInclusaoDTO contextMessage)
    {
        _logger.LogInformation("Iniciando o processamento da mensagem INSERT : {messageId}", contextMessage.MessageId);
        _mediator.Send(new CriarContatoCommand(
        
            contextMessage.Nome,
            contextMessage.Telefone,
            contextMessage.Email,
            contextMessage.Ddd
        ));
        
        return Task.CompletedTask;
    }
}