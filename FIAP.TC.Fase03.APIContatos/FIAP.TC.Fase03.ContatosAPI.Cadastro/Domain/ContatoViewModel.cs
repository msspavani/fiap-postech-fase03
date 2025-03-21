using System.Text.Json.Serialization;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;

public class ContatoViewModel
{
    public Guid ContatoId { get; set;  }
    public string Nome { get; set;  }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Ddd { get; set; }
}


public class ContatoDto(Guid contatoId, string nome, string telefone, string email, string ddd)
{
    public Ulid MessageId { get; set; } = Ulid.NewUlid();
    public Guid ContatoId { get; } = contatoId;
    public string Nome { get; } = nome;
    public string Telefone { get; } = telefone;
    public string Email { get; } = email;
    public string Ddd { get; } = ddd;
}

public class ContatoRemoveDto (string contatoId)
{
    public Ulid MessageId { get; set; } = Ulid.NewUlid();
    public Guid ContatoId { get; } = new Guid(contatoId);   
}