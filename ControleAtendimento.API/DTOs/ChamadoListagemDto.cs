namespace ControleAtendimento.API.DTOs;

public class ChamadoListagemDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public string Prioridade { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public double TempoTotalHoras { get; set; }
    public bool Atrasado { get; set; }
}
