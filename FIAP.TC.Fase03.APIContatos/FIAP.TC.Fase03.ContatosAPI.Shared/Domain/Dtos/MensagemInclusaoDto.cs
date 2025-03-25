namespace FIAP.TC.Fase03.ContatosAPI.Shared.Domain.Dtos;

public class MensagemInclusaoDTO
{
    public MensagemInclusaoDTO(Ulid messageId, string nome, string telefone, 
        string email, string ddd)
    {
        MessageId = messageId;
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Ddd = ddd;
    }

    public Ulid MessageId { get; set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public string Ddd { get; private set; }
}