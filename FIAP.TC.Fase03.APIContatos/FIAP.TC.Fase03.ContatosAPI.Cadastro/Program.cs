using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Application;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;
using MassTransit;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfgRaw) =>
            {
                var cfg = (IRabbitMqBusFactoryConfigurator)cfgRaw;

                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // Usa a exchange 'Fiap.Fase03' para a mensagem MensagemEnvelope
                cfg.Message<MensagemEnvelope>(x =>
                {
                    x.SetEntityName("Fiap.Fase03"); // não será criada, apenas usada
                });

                cfg.Send<MensagemEnvelope>((IRabbitMqMessageSendTopologyConfigurator<MensagemEnvelope> x) =>
                {
                    x.UseRoutingKeyFormatter(context =>
                        context.Headers.TryGetHeader("RoutingKey", out var value)
                            ? value?.ToString() ?? "Create"
                            : "Create"
                    );

                    // 🔴 NÃO use ConfigureConsumeTopology aqui — não existe nesse contexto
                });

                // ⚠️ NÃO configure endpoints aqui — esse é só um producer
            });
        });





        builder.Services.AddScoped<IContatoService, ContatoService>();
        builder.Services.AddScoped<CadastroProducer>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}