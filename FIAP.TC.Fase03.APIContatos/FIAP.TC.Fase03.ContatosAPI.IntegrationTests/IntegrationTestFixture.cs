using System.Reflection;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Consumers;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Services;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;
using FIAP.TC.Fase03.ContatosAPI.IntegrationTests.Containers;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Program = FIAP.TC.Fase03.ContatosAPI.Cadastro.Program;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests;

public class IntegrationTestFixture : IAsyncLifetime
{
    public RabbitMQContainerManager RabbitMq { get; private set; }

    public HttpClient ApiClient { get; private set; }

    private IHost _workerHost;
    private WebApplicationFactory<Program> _apiFactory;

    public async Task InitializeAsync()
    {
        RabbitMq = new RabbitMQContainerManager();
        await RabbitMq.StartAsync();

        var rabbitUri = new Uri(RabbitMq.GetAmqpConnectionString());

        // Iniciar a API (Program da API de cadastro)
        _apiFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["RabbitMq:Host"] = rabbitUri.Host,
                    ["RabbitMq:Port"] = rabbitUri.Port.ToString()
                });
            });
        });

        ApiClient = _apiFactory.CreateClient();

        // Iniciar o Worker
        _workerHost = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                // registrar dependências reais ou fakes
                services.AddHostedService<Worker>(); // seu worker
                services.AddScoped<IContatoRepository, FakeContatoRepository>();
                services.AddScoped<IAtualizacaoService, AtualizacaoService>();
                
                services.AddMediator(cfg =>
                {
                    cfg.AddConsumers(Assembly.GetExecutingAssembly());
                });


                services.AddMassTransit(x =>
                {
                    x.AddConsumer<ConsumerAtualizacao>();
                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(rabbitUri);
                        cfg.ReceiveEndpoint("Update", e =>
                        {
                            e.ConfigureConsumer<ConsumerAtualizacao>(ctx);
                        });
                    });
                });
            })
            .Build();

        await _workerHost.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _workerHost.StopAsync();
        await RabbitMq.StopAsync();
    }
}