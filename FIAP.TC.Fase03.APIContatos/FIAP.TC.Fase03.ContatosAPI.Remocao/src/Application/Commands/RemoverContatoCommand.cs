using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Commands;

public class RemoverContatoCommand : IRequest
{
    public Guid ContatoId { get; }

    public RemoverContatoCommand(Guid contatoId)
    {
        ContatoId = contatoId;
    }
}