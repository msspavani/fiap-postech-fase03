using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;
using MediatR;
using Newtonsoft.Json;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Consumers;

public class ConsumerRemocao : IConsumer<MensagemEnvelopeRemove>
{
    
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsumerRemocao> _logger;

    public ConsumerRemocao(ILogger<ConsumerRemocao> logger, IRemocaoService remocaoService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        
    }

    public async Task Consume(ConsumeContext<MensagemEnvelopeRemove> context)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var element = (JsonElement)context.Message.Payload;
            var payload = JsonConvert.DeserializeObject<RemocaoContatoDto>(element.GetRawText());

            _logger.LogInformation("Mensagem recebida no INSERT worker: {@Payload}", payload);

            if (payload != null)
            {
                var command = new RemoverContatoCommand(payload.ContatoId);
                await mediator.Send(command); 
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar mensagem no INSERT worker.");
            throw;
        }

        
    }
}