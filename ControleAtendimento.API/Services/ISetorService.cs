using ControleAtendimento.API.Models;

namespace ControleAtendimento.API.Services;

public interface ISetorService
{
    Setor Criar(Setor setor);
    IEnumerable<Setor> Listar();
}
