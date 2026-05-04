using ControleAtendimento.API.Data;
using ControleAtendimento.API.Models;

namespace ControleAtendimento.API.Services;

public class PrioridadeService : IPrioridadeService
{
    private readonly AppDbContext _context;

    public PrioridadeService(AppDbContext context)
    {
        _context = context;
    }

    public Prioridade Criar(Prioridade prioridade)
    {
        _context.Prioridades.Add(prioridade);
        _context.SaveChanges();
        return prioridade;
    }

    public IEnumerable<Prioridade> Listar()
    {
        return _context.Prioridades.ToList();
    }
}
