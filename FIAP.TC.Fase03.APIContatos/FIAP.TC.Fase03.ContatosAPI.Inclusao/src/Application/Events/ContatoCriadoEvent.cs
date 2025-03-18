using FIAP.TC.FASE01.APIContatos.Domain.Interfaces;
using MediatR;

namespace FIAP.TC.FASE01.APIContatos.Domain.Events;

public class ContatoCriadoEvent : IDomainEvent, INotification
{
    public Guid ContatoId { get; }
    public string Nome { get; }
    public string Email { get; }
    public DateTime DataOcorrencia { get; }

    public ContatoCriadoEvent(Guid contatoId, string nome, string email)
    {
        ContatoId = contatoId;
        Nome = nome;
        Email = email;
        DataOcorrencia = DateTime.UtcNow;
    }
}