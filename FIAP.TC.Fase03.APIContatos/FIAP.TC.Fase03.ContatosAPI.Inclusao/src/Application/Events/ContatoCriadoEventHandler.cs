using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Events;

public class ContatoCriadoEventHandler(ILogger<ContatoCriadoEventHandler> logger)
    : INotificationHandler<ContatoCriadoEvent>
{
    public Task Handle(ContatoCriadoEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Contato ID {id}, {Nome}, {email} criado com sucesso", 
            notification.ContatoId, notification.Nome, notification.Email);
        
        return Task.CompletedTask;
    }
}