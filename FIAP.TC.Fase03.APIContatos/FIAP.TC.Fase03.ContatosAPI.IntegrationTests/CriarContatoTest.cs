using System.Net.Http.Json;
using FIAP.TC.Fase03.ContatosAPI.IntegrationTests.Helpers;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests;

public class CriarContatoTest
{
    private readonly IntegrationTestFixture _fixture;

    public CriarContatoTest(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Post_Deve_Criar_Contato_Com_Sucesso()
    {
        // Arrange
        var contato = new
        {
            nome = "Maria Teste",
            email = "maria@teste.com",
            telefone = "11999999999",
            ddd = "11"
        };

        // Act
        var response = await _fixture.ApiClient.PostAsJsonAsync("/cadastro/criar", contato);

        // Assert HTTP
        response.EnsureSuccessStatusCode();

        // Aguarda o worker processar a fila
        await WaitHelper.WaitForConditionAsync(() =>
        {
            return Task.FromResult(FakeContatoRepository.ContatoCriado);
        });

        Assert.True(FakeContatoRepository.ContatoCriado);
    }
}