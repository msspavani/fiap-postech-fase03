using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Application.DTOs;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Application;

public interface IAtualizacaoService
{
    Task Processar(AtualizacaoContatoDto contextMessage);
}