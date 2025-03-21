using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Application;
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

        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            
            

            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", 5672, "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
               
                cfg.Message<AtualizacaoContatoDto>(x 
                    => x.SetEntityName("Fiap.Fase03"));

                cfg.Publish<AtualizacaoContatoDto>(x
                    =>
                {
                    x.ExchangeType = "direct";
                    
                });
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