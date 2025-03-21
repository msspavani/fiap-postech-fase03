using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces;
using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Events;

public class ContatoRemovidoEvent : IDomainEvent, INotification
{
    public Guid ContatoId { get; }
    public DateTime DataOcorrencia { get; }

    public ContatoRemovidoEvent(Guid contatoId)
    {
        ContatoId = contatoId;
        DataOcorrencia = DateTime.UtcNow;
    }
}