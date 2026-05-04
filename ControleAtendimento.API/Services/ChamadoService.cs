using ControleAtendimento.API.Data;
using ControleAtendimento.API.DTOs;
using ControleAtendimento.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleAtendimento.API.Services;

public class ChamadoService : IChamadoService
{
    private readonly AppDbContext _context;

    public ChamadoService(AppDbContext context)
    {
        _context = context;
    }

    public ServiceResult<Chamado> Criar(Chamado chamado)
    {
        chamado.Status = StatusChamado.Aberto;
        chamado.DataCriacao = DateTime.Now;

        _context.Chamados.Add(chamado);
        _context.SaveChanges();

        return ServiceResult<Chamado>.Ok(chamado);
    }

    public IEnumerable<ChamadoListagemDto> Listar()
    {
        var chamados = _context.Chamados
            .Include(c => c.Setor)
            .Include(c => c.Prioridade)
            .ToList();

        return chamados.Select(c =>
        {
            double tempoTotal = 0;

            if (c.DataInicio.HasValue)
            {
                var fim = c.DataFim ?? DateTime.Now;
                tempoTotal = (fim - c.DataInicio.Value).TotalHours;
            }

            bool atrasado = c.DataInicio.HasValue &&
                            tempoTotal > c.Prioridade.TempoEstimadoHoras;

            return new ChamadoListagemDto
            {
                Id = c.Id,
                Titulo = c.Titulo,
                Setor = c.Setor.Nome,
                Prioridade = c.Prioridade.Nome,
                Status = c.Status.ToString(),
                TempoTotalHoras = Math.Round(tempoTotal, 2),
                Atrasado = atrasado
            };
        });
    }

    public ServiceResult<Chamado> Iniciar(int id)
    {
        var chamado = _context.Chamados.Find(id);

        if (chamado == null)
            return ServiceResult<Chamado>.NaoEncontrado("Chamado não encontrado.");

        if (chamado.Status == StatusChamado.Finalizado ||
            chamado.Status == StatusChamado.Cancelado)
            return ServiceResult<Chamado>.Falha("Não é possível iniciar um chamado finalizado ou cancelado.");

        if (chamado.Status == StatusChamado.EmAtendimento)
            return ServiceResult<Chamado>.Falha("O chamado já está em atendimento.");

        chamado.DataInicio = DateTime.Now;
        chamado.Status = StatusChamado.EmAtendimento;

        _context.SaveChanges();

        return ServiceResult<Chamado>.Ok(chamado);
    }

    public ServiceResult<Chamado> Finalizar(int id, string solucao)
    {
        var chamado = _context.Chamados.Find(id);

        if (chamado == null)
            return ServiceResult<Chamado>.NaoEncontrado("Chamado não encontrado.");

        if (chamado.DataInicio == null)
            return ServiceResult<Chamado>.Falha("O chamado precisa ser iniciado antes de finalizar.");

        chamado.DataFim = DateTime.Now;
        chamado.Solucao = solucao;
        chamado.Status = StatusChamado.Finalizado;

        _context.SaveChanges();

        return ServiceResult<Chamado>.Ok(chamado);
    }
}
