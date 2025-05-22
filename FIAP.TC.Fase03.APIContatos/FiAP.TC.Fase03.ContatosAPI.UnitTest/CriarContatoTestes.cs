using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Events;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;
using MediatR;
using Moq;

namespace FiAP.TC.Fase03.ContatosAPI.UnitTest;

public class CriarContatoTestes
{
    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CriarContatoCommandHandler _handler;

    public CriarContatoTestes()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new CriarContatoCommandHandler(_contatoRepositoryMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_DeveCriarContato_ComSucesso()
    {
        // Arrange
        var command = new CriarContatoCommand("João", "123456789", "joao@email.com", "11");

        _contatoRepositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Contato>())).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _contatoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Contato>()), Times.Once);
        _mediatorMock.Verify(m => m.Publish(It.IsAny<ContatoCriadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}