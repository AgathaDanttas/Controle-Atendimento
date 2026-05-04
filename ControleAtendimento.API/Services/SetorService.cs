using ControleAtendimento.API.Data;
using ControleAtendimento.API.Models;

namespace ControleAtendimento.API.Services;

public class SetorService : ISetorService
{
    private readonly AppDbContext _context;

    public SetorService(AppDbContext context)
    {
        _context = context;
    }

    public Setor Criar(Setor setor)
    {
        _context.Setores.Add(setor);
        _context.SaveChanges();
        return setor;
    }

    public IEnumerable<Setor> Listar()
    {
        return _context.Setores.ToList();
    }
}
