using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;
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
        _ = _producer.PublishMessageAsync(
            nameof(Create), 
            new ContatoDto(contato.ContatoId, contato.Nome, contato.Telefone, contato.Email, contato.Ddd)
        );
        
        return default;
    }

    public ValueTask Update(ContatoViewModel contato)
    {
        _ = _producer.PublishMessageAsync(
            nameof(Update), 
            new ContatoDto(contato.ContatoId, contato.Nome, contato.Telefone, contato.Email, contato.Ddd)
        );
        
        return default;
    }

    public ValueTask Remove(string everything)
    {
        _ = _producer.PublishMessageAsync(
            nameof(Remove), 
            new ContatoRemoveDto(everything)
        );
        
        return default;
    }
}