using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Application;

public class ContatoService : IContatoService
{
    private readonly CadastroProducer _producer;

    public ContatoService(CadastroProducer producer)
    {
        _producer = producer;
    }

    public ValueTask Create(ContatoViewModel? contato)
    {
        _ = _producer.PublishMessageAsync<MensagemEnvelopeCreate>(
            nameof(Create), 
            new MensagemEnvelopeCreate()
            {
                Payload = new ContatoDto(contato.ContatoId, contato.Nome, contato.Telefone, contato.Email, contato.Ddd)
            }
        );
        
        return default;
    }

    public ValueTask Update(ContatoViewModel contato, string everything)
    {
        _ = _producer.PublishMessageAsync<MensagemEnvelopeUpdate>(
            nameof(Update), 
            new MensagemEnvelopeUpdate()
            {
                Payload = new ContatoDto(new Guid(everything), contato.Nome, contato.Telefone, contato.Email, contato.Ddd)
            }
        );
        
        return default;
    }

    public ValueTask Remove(string everything)
    {
        _ = _producer.PublishMessageAsync<MensagemEnvelopeRemove>(
            nameof(Remove), 
            new MensagemEnvelopeRemove(){ Payload = new ContatoRemoveDto(everything)}
        );
        
        return default;
    }
}