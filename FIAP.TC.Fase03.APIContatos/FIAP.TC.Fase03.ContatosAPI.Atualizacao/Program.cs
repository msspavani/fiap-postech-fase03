using FIAP.TC.Fase03.ContatosAPI.Atualizacao;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();