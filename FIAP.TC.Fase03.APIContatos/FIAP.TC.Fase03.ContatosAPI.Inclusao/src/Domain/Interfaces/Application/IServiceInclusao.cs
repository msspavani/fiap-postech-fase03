using FIAP.TC.Fase03.ContatosAPI.Inclusao.Application.DTOs;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Application;

public interface IServiceInclusao
{
    Task Processar(MensagemInclusaoDTO contextMessage);
}