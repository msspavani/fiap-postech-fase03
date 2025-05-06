using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Commands;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Application;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Consumers;

public class ConsumerInclusao : IConsumer<MensagemEnvelopeCreate>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsumerInclusao> _logger;

    public ConsumerInclusao(IServiceProvider serviceProvider, ILogger<ConsumerInclusao> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<MensagemEnvelopeCreate> context)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var element = (JsonElement)context.Message.Payload;
            var payload = JsonConvert.DeserializeObject<MensagemInclusaoDTO>(element.GetRawText());

            _logger.LogInformation("Mensagem recebida no INSERT worker: {@Payload}", payload);

            if (payload != null)
            {
                var command = new CriarContatoCommand(payload.Nome, payload.Telefone, payload.Email, payload.Ddd);
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