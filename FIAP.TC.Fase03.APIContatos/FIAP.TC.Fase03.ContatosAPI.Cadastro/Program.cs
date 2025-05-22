using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Application;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Infrastructure;
using FIAP.TC.FASE03.Shared.Library.Models;
using MassTransit;
using Serilog;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
            .WriteTo.Console()
            // .WriteTo.Elasticsearch(
            //     new ElasticsearchSinkOptions(( new Uri("EnderecoDoElasticSearch"))
            //     {
            //         AutoRegisterTemplate = true,
            //         IndexFormat = "worker-inclusao-logs-{0:yyyy.MM.dd}"
            //     })
            .CreateLogger();


        try
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
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.Message<MensagemEnvelopeCreate>(m => { m.SetEntityName("Fiap.Fase03.Create"); });
                    cfg.Publish<MensagemEnvelopeCreate>(p => { p.ExchangeType = "direct"; });

                    cfg.Message<MensagemEnvelopeUpdate>(m => m.SetEntityName("Fiap.Fase03.Update"));
                    cfg.Publish<MensagemEnvelopeUpdate>(p => p.ExchangeType = "direct");

                    cfg.Message<MensagemEnvelopeRemove>(m => m.SetEntityName("Fiap.Fase03.Remove"));
                    cfg.Publish<MensagemEnvelopeRemove>(p => p.ExchangeType = "direct");
                    

                    cfg.Send<MensagemEnvelopeCreate>(s =>
                    {
                        s.UseRoutingKeyFormatter(context =>
                            context.Headers.Get<string>("RoutingKey") ?? "Create"
                        );
                    });
                    
                    cfg.Send<MensagemEnvelopeUpdate>(s =>
                    {
                        s.UseRoutingKeyFormatter(ctx => ctx.Headers.Get<string>("RoutingKey") ?? "Update");
                    });

                    cfg.Send<MensagemEnvelopeRemove>(s =>
                    {
                        s.UseRoutingKeyFormatter(ctx => ctx.Headers.Get<string>("RoutingKey") ?? "Remove");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });



            builder.Services.AddScoped<IContatoService, ContatoService>();
            builder.Services.AddScoped<CadastroProducer>();



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.MapControllers();
            app.UseHttpsRedirection();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "A Aplicacao Falhou ao iniciar");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}