using Microsoft.AspNetCore.Mvc;
using ControleAtendimento.API.Models;
using ControleAtendimento.API.Services;

namespace ControleAtendimento.API.Controllers;

[ApiController]
[Route("prioridades")]
public class PrioridadesController : ControllerBase
{
    private readonly IPrioridadeService _prioridadeService;

    public PrioridadesController(IPrioridadeService prioridadeService)
    {
        _prioridadeService = prioridadeService;
    }

    [HttpPost]
    public IActionResult Criar(Prioridade prioridade)
    {
        var criada = _prioridadeService.Criar(prioridade);
        return Created("", criada);
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_prioridadeService.Listar());
    }
}