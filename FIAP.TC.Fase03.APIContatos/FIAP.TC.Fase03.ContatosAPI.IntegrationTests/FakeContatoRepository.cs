using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests;

public class FakeContatoRepository : IContatoRepository
{
    public static bool ContatoAtualizado = false;

    public Task<Contato> ObterPorIdAsync(Guid id)
    {
        return Task.FromResult(new Contato("Antigo", "antigo@email.com", "11911112222", "11"));
    }

    public Task AtualizarAsync(Contato contato)
    {
        ContatoAtualizado = true;
        return Task.CompletedTask;
    }
}