using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using MediatR;
using Moq;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests;

public class RemoverContatoTests
{

    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly RemoverContatoCommandHandler _handler;

    public RemoverContatoTests()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new RemoverContatoCommandHandler(_contatoRepositoryMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ContatoExiste_DeveRemoverEPublicarEvento()
    {
        // Arrange
        var contatoId = Guid.NewGuid();
        var command = new RemoverContatoCommand(contatoId);

        _contatoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(contatoId))
            .ReturnsAsync(new Contato()); // Simulando que o contato existe

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _contatoRepositoryMock.Verify(repo => repo.RemoverAsync(contatoId), Times.Once);
        _mediatorMock.Verify(m => m.Publish(It.Is<ContatoRemovidoEvent>(
            e => e.ContatoId == contatoId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ContatoNaoExiste_DeveLancarExcecao()
    {
        // Arrange
        var contatoId = Guid.NewGuid();
        var command = new RemoverContatoCommand(contatoId);

        _contatoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(contatoId))
            .ReturnsAsync((Contato)null); // Contato não encontrado

        // Act & Assert
        var excecao = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Contato não encontrado.", excecao.Message);

        _contatoRepositoryMock.Verify(repo => repo.RemoverAsync(It.IsAny<Guid>()), Times.Never);
        _mediatorMock.Verify(m => m.Publish(It.IsAny<ContatoRemovidoEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

}