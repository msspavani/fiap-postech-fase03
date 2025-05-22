using System.Data;
using System.Reflection;
using FIAP.TC.Fase03.ContatosAPI.Remocao;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Consumers;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Services;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Infrastructure.Repositories;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;
using Microsoft.Data.SqlClient;
using Prometheus;


var metricServer = new KestrelMetricServer(port: 9097); 
metricServer.Start();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();



builder.Services.AddTransient<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    return new SqlConnection(connectionString);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("FIAP.TC.FASE03.ContatosAPI.Remocao"))
);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IRemocaoService, ServiceRemocao>();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    // Registra apenas o consumer da fila de Delete
    x.AddConsumer<ConsumerRemocao>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        // Declara a exchange Fiap.Fase03.Remove como tipo direct
        cfg.Message<MensagemEnvelopeRemove>(m =>
        {
            m.SetEntityName("Fiap.Fase03.Remove");
        });

        
        cfg.ReceiveEndpoint("RemoveQueue", e =>
        {
            e.ConfigureConsumeTopology = false;

            e.ConfigureConsumer<ConsumerRemocao>(context);

            e.Bind("Fiap.Fase03.Remove", x =>
            {
                x.RoutingKey = "Remove";
                x.ExchangeType = "direct";
            });
        });
    });
});


var host = builder.Build();
host.Run();