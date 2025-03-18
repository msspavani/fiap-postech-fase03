namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;

public class AtualizacaoContatoDto
{
    public Ulid MessageId { get; set; }
    public Guid ContatoId { get; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public string Ddd { get; private set; }
}