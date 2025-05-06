using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Events;

public class ContatoRemovidoEventHandler(ILogger<ContatoRemovidoEventHandler> logger) : INotificationHandler<ContatoRemovidoEvent>
{
    public Task Handle(ContatoRemovidoEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Contato ID {id} removido com sucesso", 
            notification.ContatoId);
        
        return Task.CompletedTask;
    }
}