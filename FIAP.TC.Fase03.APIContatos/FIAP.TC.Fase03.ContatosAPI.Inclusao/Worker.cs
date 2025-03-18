using MassTransit;
using RabbitMQ.Client;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao;

public class Worker : BackgroundService
{
    private readonly IBusControl _bus;
    private readonly ILogger<Worker> _logger;

    public Worker(IBusControl bus, ILogger<Worker> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.StartAsync(stoppingToken);
        _logger.LogInformation("Consumer for INSERT waiting messages...");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _bus.StopAsync(cancellationToken);
    }
}