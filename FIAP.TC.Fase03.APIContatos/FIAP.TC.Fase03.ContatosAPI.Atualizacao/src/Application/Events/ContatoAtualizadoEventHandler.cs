using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Events;

public class ContatoAtualizadoEventHandler(ILogger<ContatoAtualizadoEventHandler> logger) : INotificationHandler<ContatoAtualizadoEvent>
{
    public Task Handle(ContatoAtualizadoEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[+] Contato ID {id}, {Nome}, {email} atualizado com sucesso", 
            notification.ContatoId, notification.Nome, notification.Email);
        
        return Task.CompletedTask;
    }
    
}