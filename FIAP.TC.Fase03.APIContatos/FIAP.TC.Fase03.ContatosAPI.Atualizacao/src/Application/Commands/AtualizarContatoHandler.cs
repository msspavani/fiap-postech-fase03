using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;
using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Commands;

public class AtualizarContatoCommandHandler : IRequestHandler<AtualizarContatoCommand>
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IMediator _mediator;

    public AtualizarContatoCommandHandler(IContatoRepository contatoRepository, IMediator mediator)
    {
        _contatoRepository = contatoRepository;
        _mediator = mediator;
    }

    public async Task Handle(AtualizarContatoCommand request, CancellationToken cancellationToken)
    {
        
        var contato = await _contatoRepository.ObterPorIdAsync(request.ContatoId);
        if (contato == null)
            throw new Exception("Contato não encontrado.");

        
        contato.Atualizar(request.Nome, request.Telefone, request.Email, request.Ddd);

        
        await _contatoRepository.AtualizarAsync(contato);
        
        var evento = new ContatoAtualizadoEvent(contato.Id, contato.Nome, contato.Email, contato.Telefone);
        await _mediator.Publish(evento, cancellationToken);
        
    }
}