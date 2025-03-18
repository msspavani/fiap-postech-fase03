using FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Entity;

namespace FIAP.TC.Fase03.ContatosAPI.Inclusao.Domain.Interfaces.Repositories;

public interface IContatoRepository
{
    Task AdicionarAsync(Contato contato);                 
}