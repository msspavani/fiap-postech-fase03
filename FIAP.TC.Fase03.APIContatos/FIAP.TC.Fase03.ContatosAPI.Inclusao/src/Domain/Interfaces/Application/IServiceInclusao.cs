using FIAP.TC.Fase03.ContatosAPI.Shared.Domain.Dtos;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Application;

public interface IServiceInclusao
{
    Task Processar(MensagemInclusaoDTO contextMessage);
}