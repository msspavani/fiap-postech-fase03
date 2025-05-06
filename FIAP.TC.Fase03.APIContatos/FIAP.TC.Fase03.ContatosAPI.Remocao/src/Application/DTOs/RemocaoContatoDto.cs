using Newtonsoft.Json;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.DTOs;

public class RemocaoContatoDto
{
    [JsonProperty("messageId")]
    public Ulid MessageId { get; set; }
   
    [JsonProperty("contatoId")]
    public Guid ContatoId { get; set; }
}