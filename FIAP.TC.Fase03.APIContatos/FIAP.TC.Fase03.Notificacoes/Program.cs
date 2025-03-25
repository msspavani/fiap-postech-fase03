using FIAP.TC.Fase03.Notificacoes;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();



// builder.Services.AddMassTransit(x =>
// {
//     x.AddConsumer<ConsumerRemocao>();
//     x.UsingRabbitMq((context, cfg) =>
//     {
//         cfg.Host("localhost", "/", host =>
//         {
//             host.Username("guest");
//             host.Password("guest");
//         });
//         
//         cfg.ReceiveEndpoint("remocao_queue", e =>
//         {
//             e.ConfigureConsumer<ConsumerRemocao>(context);
//         });
//         
//     });
// });



var host = builder.Build();
host.Run();