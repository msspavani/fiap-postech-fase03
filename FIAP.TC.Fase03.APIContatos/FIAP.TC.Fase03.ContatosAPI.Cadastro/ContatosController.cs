using System.Text.Json;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain;
using FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TC.Fase03.ContatosAPI.Cadastro;

[ApiController]
[Route("cadastro")]
public class ContatosController : ControllerBase
{
    private readonly ILogger<ContatosController> _logger;
    private readonly IContatoService _contatoService;

    public ContatosController(ILogger<ContatosController> logger, IContatoService contatoService)
    {
        _logger = logger;
        _contatoService = contatoService;
    }
    
    [HttpPost("{everything}")]
    public async ValueTask<IActionResult> CriarContato([FromBody] ContatoViewModel? contato)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _contatoService.Create(contato);
        return Ok();
    }

    [HttpPut("{everything}")]
    public async ValueTask<IActionResult> AtualizarContato([FromBody] ContatoViewModel? contato)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(kv => kv.Value.Errors.Count > 0)
                .ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToList()
                );
            Console.WriteLine("ModelState inválido:");
            Console.WriteLine(JsonSerializer.Serialize(errors));
            return BadRequest(ModelState);
        }

        Console.WriteLine($"Recebido: {JsonSerializer.Serialize(contato)}");


        await _contatoService.Update(contato);
        _logger.LogInformation("Contato enviado para atualização.");

        return Ok();
    }

    [HttpDelete("{everything}")]
    public async ValueTask<IActionResult> RemoverContato(string everything)
    {
        if (string.IsNullOrEmpty(everything))
        {
            return BadRequest("O ID do contato deve ser informado para remoção.");
        }

        await _contatoService.Remove(everything);
        
        _logger.LogInformation("Contato enviado para remoção.");

        return Ok();
    }
    
    
}