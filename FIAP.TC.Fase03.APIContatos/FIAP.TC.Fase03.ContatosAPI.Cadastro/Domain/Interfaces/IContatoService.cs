namespace FIAP.TC.Fase03.ContatosAPI.Cadastro.Domain.Interfaces;

public interface IContatoService
{
    ValueTask Create(ContatoViewModel? contato);
    ValueTask Update(ContatoViewModel contato);
    ValueTask Remove(string everything);
}