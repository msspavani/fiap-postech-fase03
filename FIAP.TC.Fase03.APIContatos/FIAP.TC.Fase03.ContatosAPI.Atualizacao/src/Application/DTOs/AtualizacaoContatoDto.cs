using Newtonsoft.Json;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;

public class AtualizacaoContatoDto
{
    [JsonProperty("messageId")]
    public Ulid MessageId { get; set; }
    
    [JsonProperty("contatoId")]
    public Guid ContatoId { get; set; }
    
    [JsonProperty("nome")]
    public string Nome { get; private set; }
    
    [JsonProperty("telefone")]
    public string Telefone { get; private set; }
    
    [JsonProperty("email")]
    public string Email { get; private set; }
    
    [JsonProperty("ddd")]
    public string Ddd { get; private set; }
}