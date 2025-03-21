using FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Entity;

namespace FIAP.TC.Fase03.ContatosAPI.Remocao.Domain.Interfaces.Repositories;

public interface IContatoRepository
{
                     
    Task RemoverAsync(Guid contatoId);                    
    Task<Contato?> ObterPorIdAsync(Guid contatoId);       
    
                    
}