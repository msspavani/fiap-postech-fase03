using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Entity;
using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;

namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests;

public class FakeContatoRepository : IContatoRepository
{
    public static TaskCompletionSource<bool> AtualizacaoTcs { get; private set; } = CreateTcs();

    private static TaskCompletionSource<bool> CreateTcs()
        => new(TaskCreationOptions.RunContinuationsAsynchronously);

    public static void Reset() => AtualizacaoTcs = CreateTcs();

    public Task AtualizarAsync(Contato contato)
    {
        Console.WriteLine("✅ AtualizarAsync foi chamado.");
        AtualizacaoTcs.TrySetResult(true);
        return Task.CompletedTask;
    }

    public Task<Contato> ObterPorIdAsync(Guid id)
    {
        // Simula que o contato existe
        return Task.FromResult(new Contato("Fulano", "999999999", "fulano@email.com", "11"));
    }

    public Task AdicionarAsync(Contato contato) => Task.CompletedTask;

    public Task RemoverAsync(Contato contato) => Task.CompletedTask;


}