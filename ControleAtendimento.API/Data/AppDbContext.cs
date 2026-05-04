using Microsoft.EntityFrameworkCore;
using ControleAtendimento.API.Models;

namespace ControleAtendimento.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Setor> Setores { get; set; }

    public DbSet<Prioridade> Prioridades { get; set; }

    public DbSet<Chamado> Chamados { get; set; }
}