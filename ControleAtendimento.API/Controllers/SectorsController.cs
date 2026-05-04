using Microsoft.AspNetCore.Mvc;
using ControleAtendimento.API.Models;
using ControleAtendimento.API.Services;

namespace ControleAtendimento.API.Controllers;

[ApiController]
[Route("setores")]
public class SetoresController : ControllerBase
{
    private readonly ISetorService _setorService;

    public SetoresController(ISetorService setorService)
    {
        _setorService = setorService;
    }

    [HttpPost]
    public IActionResult Criar(Setor setor)
    {
        var criado = _setorService.Criar(setor);
        return Created("", criado);
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_setorService.Listar());
    }
}
