using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Commands;


public class RemoverContatoCommandHandler : IRequestHandler<RemoverContatoCommand>
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IMediator _mediator;

    public RemoverContatoCommandHandler(IContatoRepository contatoRepository, IMediator mediator)
    {
        _contatoRepository = contatoRepository;
        _mediator = mediator;
    }

    public async Task Handle(RemoverContatoCommand request, CancellationToken cancellationToken)
    {
        
        var contato = await _contatoRepository.ObterPorIdAsync(request.ContatoId);
        if (contato == null)
            throw new Exception("Contato não encontrado.");
        
        await _contatoRepository.RemoverAsync(request.ContatoId);
        
        var evento = new ContatoRemovidoEvent(request.ContatoId);
        await _mediator.Publish(evento, cancellationToken);
        
        
    }
}