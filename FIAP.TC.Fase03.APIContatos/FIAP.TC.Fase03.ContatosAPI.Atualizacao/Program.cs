using System.Data;
using System.Reflection;
using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Consumers;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.Services;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Data.SqlClient;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


builder.Services.AddTransient<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    return new SqlConnection(connectionString);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("FIAP.TC.FASE03.ContatosAPI.Atualizacao"))
);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IAtualizacaoService, AtualizacaoService>();


builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(Assembly.GetExecutingAssembly());
});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddConsumer<ConsumerAtualizacao>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        
        
        cfg.ReceiveEndpoint("Update", e =>
        {
            e.ConfigureConsumer<ConsumerAtualizacao>(context);
            e.Bind("Fiap.Fase03", x =>
            {
                x.RoutingKey = "Update";
                x.ExchangeType = "direct";
            });
        });
        
    });
});

var host = builder.Build();
host.Run();