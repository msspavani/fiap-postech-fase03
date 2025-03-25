using FIAP.TC.FASE01.APIContatos.Domain.Events;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;
using MediatR;
using Moq;

namespace FiAP.TC.Fase03.ContatosAPI.UnitTest;

public class CriarContatoTestes
{
    [Fact]
    public async Task Handle_Deve_AdicionarContato_E_PublicarEvento()
    {
        // Arrange
        var contatoRepositoryMock = new Mock<IContatoRepository>();
        var mediatorMock = new Mock<IMediator>();

        var handler = new CriarContatoCommandHandler(contatoRepositoryMock.Object, mediatorMock.Object);

        var command = new CriarContatoCommand("João", "11999999999", "joao@email.com", "11");

        // Act
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        contatoRepositoryMock.Verify(repo =>
            repo.AdicionarAsync(It.Is<Contato>(c =>
                c.Nome == command.Nome &&
                c.Email == command.Email &&
                c.Telefone == command.Telefone &&
                c.Ddd == command.Ddd
            )), Times.Once);

        mediatorMock.Verify(m =>
            m.Publish(It.Is<ContatoCriadoEvent>(e =>
                e.Nome == command.Nome &&
                e.Email == command.Email &&
                e.ContatoId != Guid.Empty
            ), It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotEqual(Guid.Empty, resultado);
    }
}