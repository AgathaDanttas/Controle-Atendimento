using ControleAtendimento.API.DTOs;
using ControleAtendimento.API.Models;

namespace ControleAtendimento.API.Services;

public interface IChamadoService
{
    ServiceResult<Chamado> Criar(Chamado chamado);
    IEnumerable<ChamadoListagemDto> Listar();
    ServiceResult<Chamado> Iniciar(int id);
    ServiceResult<Chamado> Finalizar(int id, string solucao);
}
