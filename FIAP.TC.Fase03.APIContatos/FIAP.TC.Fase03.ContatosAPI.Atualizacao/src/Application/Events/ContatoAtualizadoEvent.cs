using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces;
using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Events;

public class ContatoAtualizadoEvent : IDomainEvent, INotification
{
    public Guid ContatoId { get; }
    public string Nome { get; }
    public string Email { get; }
    public string Telefone { get; }
    public DateTime DataOcorrencia { get; }

    public ContatoAtualizadoEvent(Guid contatoId, string nome, string email, string telefone)
    {
        ContatoId = contatoId;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        DataOcorrencia = DateTime.UtcNow;
    }
}