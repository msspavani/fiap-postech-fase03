using System.Net.Http.Json;
using FIAP.TC.Fase03.ContatosAPI.IntegrationTests.Helpers;
using FluentAssertions;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests;

public class AtualizarContatoTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IntegrationTestFixture _fixture;

    public AtualizarContatoTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Requisicao_HTTP_Deve_Executar_Handler()
    {
        var contato = new
        {
            ContatoId = Guid.NewGuid(),
            Nome = "João Teste",
            Email = "joao@email.com",
            Telefone = "11988887777",
            Ddd = "11"
        };

        var response = await _fixture.ApiClient.PutAsJsonAsync("/cadastro/atualizar", contato);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Erro: " + content);
        response.EnsureSuccessStatusCode();

        await WaitHelper.WaitForConditionAsync(() =>
        {
            return Task.FromResult(FakeContatoRepository.ContatoAtualizado);
        });

        Assert.True(FakeContatoRepository.ContatoAtualizado);
    }
}