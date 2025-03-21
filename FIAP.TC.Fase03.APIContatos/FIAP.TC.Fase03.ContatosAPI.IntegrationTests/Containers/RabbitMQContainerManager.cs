using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Testcontainers.RabbitMq;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests.Containers;

public class RabbitMQContainerManager
{
    public RabbitMqContainer Container { get; private set; }

    public async Task StartAsync()
    {
        Container = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithPortBinding(15672, true)
            .WithPortBinding(5672, true)
            .Build();

        await Container.StartAsync();
    }

    public string GetAmqpConnectionString() =>
        $"amqp://guest:guest@localhost:{Container.GetMappedPublicPort(15672)}";

    public async Task StopAsync() => await Container.DisposeAsync();
}