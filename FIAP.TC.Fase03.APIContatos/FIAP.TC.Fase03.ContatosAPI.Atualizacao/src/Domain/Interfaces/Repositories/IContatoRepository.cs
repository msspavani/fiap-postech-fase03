using FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Entity;

namespace FIAP.TC.Fase03.ContatosAPI.Atualizacao.Domain.Interfaces.Repositories;

public interface IContatoRepository
{
                     
    Task AtualizarAsync(Contato contato);                 
    Task<Contato?> ObterPorIdAsync(Guid contatoId);       
    
                    
}