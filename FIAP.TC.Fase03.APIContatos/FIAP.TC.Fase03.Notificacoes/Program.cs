using FIAP.TC.Fase03.Notificacoes;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();






var host = builder.Build();
host.Run();