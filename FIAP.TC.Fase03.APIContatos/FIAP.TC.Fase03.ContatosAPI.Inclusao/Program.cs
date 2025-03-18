using System.Data;
using System.Reflection;
using FIAP.TC.Fase03.ContatosAPI.Inclusao;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Consumers;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.Services;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Application;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;
using FIAP.TC.Fase03.ContatosAPI.Inclusao.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Data.SqlClient;
using MediatR;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    return new SqlConnection(connectionString);
});

builder.Services.AddMediatR(cfg =>
      cfg.RegisterServicesFromAssembly(Assembly.Load("FIAP.TC.FASE03.ContatosAPI.Inclusao"))
  );

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IServiceInclusao, InclusaoService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumerInclusao>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        
        cfg.ReceiveEndpoint("inclusao_queue", e =>
        {
            e.ConfigureConsumer<ConsumerInclusao>(context);
        });
        
    });
});


var host = builder.Build();
host.Run();