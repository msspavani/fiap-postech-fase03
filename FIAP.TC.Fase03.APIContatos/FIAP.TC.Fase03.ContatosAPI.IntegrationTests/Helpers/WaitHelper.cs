namespace FIAP.TC.Fase03.ContatosAPI.IntegrationTests.Helpers;

public static class WaitHelper
{
    public static async Task WaitForConditionAsync(Func<Task<bool>> condition, int timeoutSeconds = 120)
    {
        var timeout = TimeSpan.FromSeconds(timeoutSeconds);
        var start = DateTime.UtcNow;

        while ((DateTime.UtcNow - start) < timeout)
        {
            if (await condition()) return;
            await Task.Delay(500);
        }

        throw new TimeoutException("Condição não satisfeita dentro do tempo limite.");
    }
}