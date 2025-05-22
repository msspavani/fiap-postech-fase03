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
    public async Task AtualizarContato_DeveRetornarSucesso()
    {
        
        // Arrange
        var contatoId = Guid.Parse("AA13420F-8B2E-4901-B273-0ACCDE853C8E");
        FakeContatoRepository.Reset();

        var atualizarContato = new
        {
            ContatoId = contatoId,
            Nome = "Fulano Atualizado",
            Email = "atualizado@email.com",
            Telefone = "11911112222",
            Ddd = "11"
        };

        // Act
        var response = await _fixture.ApiClient.PutAsJsonAsync("/cadastro/atualizar", atualizarContato);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("📝 Conteúdo da resposta:\n" + content);

        response.EnsureSuccessStatusCode();

        // Assert - 
        await FakeContatoRepository.AtualizacaoTcs.Task.WaitAsync(TimeSpan.FromSeconds(60));
        Assert.True(FakeContatoRepository.AtualizacaoTcs.Task.IsCompletedSuccessfully);
    }


}