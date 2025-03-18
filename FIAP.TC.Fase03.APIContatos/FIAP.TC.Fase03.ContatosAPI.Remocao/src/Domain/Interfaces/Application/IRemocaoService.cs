using FIAP.TC.Fase03.ContatosAPI.Remocao.Application.DTOs;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Application;

public interface IRemocaoService
{
    Task Processar(RemocaoContatoDto contextMessage);
}