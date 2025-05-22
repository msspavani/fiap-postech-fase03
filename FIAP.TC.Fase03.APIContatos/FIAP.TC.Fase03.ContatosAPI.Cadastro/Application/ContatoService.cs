using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;
using Prometheus;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Application;

public class ContatoService : IContatoService
{
    private readonly CadastroProducer _producer;

    private static readonly Counter MensagensPublicadasTotal = Metrics
        .CreateCounter("cadastro_mensagens_publicadas_total", "Total de mensagens publicadas na fila", 
            new CounterConfiguration { LabelNames = new[] { "tipo" } });

    private static readonly Histogram TempoPublicacaoMensagem = Metrics
        .CreateHistogram("cadastro_tempo_publicacao_segundos", "Tempo de publicação da mensagem na fila",
            new HistogramConfiguration { LabelNames = new[] { "tipo" } });
    public ContatoService(CadastroProducer producer)
    {
        _producer = producer;
    }

    public ValueTask Create(ContatoViewModel? contato)
    {
        return PublicarComMetricasAsync(nameof(Create), new MensagemEnvelopeCreate
        {
            Payload = new ContatoDto(contato.ContatoId, contato.Nome, contato.Telefone, contato.Email, contato.Ddd)
        });
    }

    public ValueTask Update(ContatoViewModel contato, string everything)
    {
        return PublicarComMetricasAsync(nameof(Update), new MensagemEnvelopeUpdate
        {
            Payload = new ContatoDto(contato.ContatoId, contato.Nome, contato.Telefone, contato.Email, contato.Ddd)
        });
    }

    public ValueTask Remove(string everything)
    {
        return PublicarComMetricasAsync(nameof(Remove), new MensagemEnvelopeRemove
        {
            Payload = new ContatoRemoveDto(everything)
        });
    }
    
    private async ValueTask PublicarComMetricasAsync<T>(string tipo, T mensagem)
        where T : class
    {
        using (TempoPublicacaoMensagem.WithLabels(tipo).NewTimer())
        {
            await _producer.PublishMessageAsync<T>(tipo, mensagem);
            MensagensPublicadasTotal.WithLabels(tipo).Inc();
        }
    }
    
}