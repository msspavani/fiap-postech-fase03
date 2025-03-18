using MediatR;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Commands;

public class AtualizarContatoCommand : IRequest
{
    public Guid ContatoId { get; }
    public string Nome { get; }
    public string Telefone { get; }
    public string Email { get; }
    public string Ddd { get; }

    public AtualizarContatoCommand(Guid contatoId, string nome, string telefone, string email, string ddd)
    {
        ContatoId = contatoId;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Ddd = ddd;
    }
}