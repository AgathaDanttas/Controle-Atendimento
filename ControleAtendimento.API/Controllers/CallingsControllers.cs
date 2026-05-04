using Microsoft.AspNetCore.Mvc;
using ControleAtendimento.API.Models;
using ControleAtendimento.API.Services;

namespace ControleAtendimento.API.Controllers;

[ApiController]
[Route("chamados")]
public class ChamadosController : ControllerBase
{
    private readonly IChamadoService _chamadoService;

    public ChamadosController(IChamadoService chamadoService)
    {
        _chamadoService = chamadoService;
    }

    [HttpPost]
    public IActionResult Criar(Chamado chamado)
    {
        var resultado = _chamadoService.Criar(chamado);
        return Created("", resultado.Dados);
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_chamadoService.Listar());
    }

    [HttpPut("{id}/iniciar")]
    public IActionResult Iniciar(int id)
    {
        var resultado = _chamadoService.Iniciar(id);

        if (!resultado.Sucesso)
            return resultado.CodigoHttp == 404
                ? NotFound(resultado.Mensagem)
                : BadRequest(resultado.Mensagem);

        return Ok(resultado.Dados);
    }

    [HttpPut("{id}/finalizar")]
    public IActionResult Finalizar(int id, [FromBody] string solucao)
    {
        var resultado = _chamadoService.Finalizar(id, solucao);

        if (!resultado.Sucesso)
            return resultado.CodigoHttp == 404
                ? NotFound(resultado.Mensagem)
                : BadRequest(resultado.Mensagem);

        return Ok(resultado.Dados);
    }
}