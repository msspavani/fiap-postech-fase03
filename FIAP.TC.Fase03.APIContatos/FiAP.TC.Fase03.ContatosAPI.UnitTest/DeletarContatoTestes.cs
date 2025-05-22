using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using MediatR;
using Moq;

namespace FiAP.TC.Fase03.ContatosAPI.UnitTest;

public class DeletarContatoTestes
{
    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly RemoverContatoCommandHandler _handler;

    public DeletarContatoTestes()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new RemoverContatoCommandHandler(_contatoRepositoryMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ContatoNaoExiste_DeveLancarExcecao()
    {
        // Arrange
        var contatoId = Guid.NewGuid();
        var command = new RemoverContatoCommand(contatoId);

        _contatoRepositoryMock.Setup(r => r.ObterPorIdAsync(contatoId)).ReturnsAsync((Contato)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_DeveRemoverContato_ComSucesso()
    {
        // Arrange
        var contatoId = Guid.NewGuid();
        var contato = new FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Entity.Contato( "Carlos", "111222333", "carlos@email.com", "31");

        _contatoRepositoryMock.Setup(r => r.ObterPorIdAsync(contato.Id)).ReturnsAsync(contato);
        _contatoRepositoryMock.Setup(r => r.RemoverAsync(contato.Id)).Returns(Task.CompletedTask);

        var command = new RemoverContatoCommand(contato.Id);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _contatoRepositoryMock.Verify(r => r.RemoverAsync(contato.Id), Times.Once);
        _mediatorMock.Verify(m => m.Publish(It.IsAny<ContatoRemovidoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
