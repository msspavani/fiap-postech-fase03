using MassTransit.Courier.Contracts;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.DTOs;

public class MensagemInclusaoDTO
{
    public Ulid MessageId { get; set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public string Ddd { get; private set; }
}