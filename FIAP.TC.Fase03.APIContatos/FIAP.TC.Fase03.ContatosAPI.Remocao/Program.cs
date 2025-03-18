using System.Data;
using System.Reflection;
using FIAP.TC.Fase03.ContatosAPI.Remocao;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Consumers;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.Services;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;
using FIAP.TC.Fase03.ContatosAPI.Remocao.Infrastructure.Repositories;
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
    cfg.RegisterServicesFromAssembly(Assembly.Load("FIAP.TC.FASE03.ContatosAPI.Remocao"))
);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IRemocaoService, ServiceRemocao>();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumerRemocao>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        
        cfg.ReceiveEndpoint("remocao_queue", e =>
        {
            e.ConfigureConsumer<ConsumerRemocao>(context);
        });
        
    });
});


var host = builder.Build();
host.Run();