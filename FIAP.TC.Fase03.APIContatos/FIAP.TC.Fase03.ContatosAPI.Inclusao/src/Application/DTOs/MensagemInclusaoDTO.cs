using MassTransit.Courier.Contracts;
using Newtonsoft.Json;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.DTOs;

public class MensagemInclusaoDTO
{
    public Ulid MessageId { get; set; }
    
    [JsonProperty("nome")]
    public string Nome { get; private set; }
    
    [JsonProperty("telefone")]
    public string Telefone { get; private set; }
    
    [JsonProperty("email")]
    public string Email { get; private set; }
    
    [JsonProperty("ddd")]
    public string Ddd { get; private set; }
}

public class MensagemEnvelope
{
    public object Payload { get; set; } = default!;
}
