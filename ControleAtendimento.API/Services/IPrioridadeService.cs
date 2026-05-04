using ControleAtendimento.API.Models;

namespace ControleAtendimento.API.Services;

public interface IPrioridadeService
{
    Prioridade Criar(Prioridade prioridade);
    IEnumerable<Prioridade> Listar();
}
