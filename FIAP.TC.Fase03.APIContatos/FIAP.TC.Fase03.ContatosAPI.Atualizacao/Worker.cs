using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRemocaoService _remocaoService;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}