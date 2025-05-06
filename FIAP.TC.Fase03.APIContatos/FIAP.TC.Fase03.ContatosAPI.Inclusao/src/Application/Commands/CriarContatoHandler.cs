using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;
using MediatR;


namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Commands;

public class CriarContatoCommandHandler : IRequestHandler<CriarContatoCommand, Guid>
{
    private readonly IContatoRepository _contatoRepository;
    private readonly IMediator _mediator;

    public CriarContatoCommandHandler(IContatoRepository contatoRepository, IMediator mediator)
    {
        _contatoRepository = contatoRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CriarContatoCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var contato = new Contato(request.Nome, request.Telefone, request.Email, request.Ddd);
            
            await _contatoRepository.AdicionarAsync(contato);
            
            var evento = new ContatoCriadoEvent(contato.Id, contato.Nome, contato.Email);
            await _mediator.Publish(evento, cancellationToken);

            return contato.Id;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}