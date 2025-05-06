using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Consumers;

public class ConsumerAtualizacao : IConsumer<MensagemEnvelopeUpdate>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsumerAtualizacao> _logger;

    public ConsumerAtualizacao(IAtualizacaoService atualizacaoService, ILogger<ConsumerAtualizacao> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Consume(ConsumeContext<MensagemEnvelopeUpdate> context)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var element = (JsonElement)context.Message.Payload;
            var payload = JsonConvert.DeserializeObject<AtualizacaoContatoDto>(element.GetRawText());

            _logger.LogInformation("Mensagem recebida no INSERT worker: {@Payload}", payload);

            if (payload != null)
            {
                var command = new AtualizarContatoCommand(payload.ContatoId,  
                                                          payload.Nome, 
                                                          payload.Telefone, 
                                                          payload.Email, 
                                                          payload.Ddd);
                
                await mediator.Send(command); 
            }
            
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar mensagem no UPDATE worker.");
            throw;
        }


    }
}