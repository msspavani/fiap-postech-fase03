using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;
using MediatR;
using Moq;
using Contato = FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Entity.Contato;

namespace FiAP.TC.Fase03.ContatosAPI.UnitTest;

public class AtualizarContatoTestes
{
    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AtualizarContatoCommandHandler _handler;
    
    public AtualizarContatoTestes()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new AtualizarContatoCommandHandler(_contatoRepositoryMock.Object, _mediatorMock.Object);
    }


    [Fact]
    public async Task Handle_ContatoNaoExiste_DeveLancarExcecao()
    {
        //Arrange
        var contatoId = Guid.NewGuid();
        var command = new AtualizarContatoCommand(contatoId, "Maria", "987654321", "maria@email.com", "21");

        _contatoRepositoryMock.Setup(r => r.ObterPorIdAsync(contatoId)).ReturnsAsync((Contato)null);
        
        //Act && Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
    
}