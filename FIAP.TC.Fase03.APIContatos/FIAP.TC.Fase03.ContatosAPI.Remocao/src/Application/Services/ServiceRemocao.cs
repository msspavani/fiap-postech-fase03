using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;
using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Services;

public class ServiceRemocao : IRemocaoService
{
    
    private readonly IMediator _mediator;
    private readonly ILogger<ServiceRemocao> _logger;

    public ServiceRemocao(IMediator mediator, ILogger<ServiceRemocao> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task Processar(RemocaoContatoDto contextMessage)
    {
        _logger.LogInformation("Iniciando o processamento da mensagem DELETE : {messageId}", contextMessage.MessageId);
        
        _mediator.Send(new RemoverContatoCommand(contextMessage.ContatoId));
        
        return Task.CompletedTask;
    }
}