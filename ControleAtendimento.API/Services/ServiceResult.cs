namespace ControleAtendimento.API.Services;

/// <summary>
/// Encapsula o resultado de uma operação de serviço,
/// separando a lógica de negócio da resposta HTTP.
/// </summary>
public class ServiceResult<T>
{
    public bool Sucesso { get; private set; }
    public string? Mensagem { get; private set; }
    public T? Dados { get; private set; }
    public int CodigoHttp { get; private set; }

    private ServiceResult() { }

    public static ServiceResult<T> Ok(T dados) =>
        new() { Sucesso = true, Dados = dados, CodigoHttp = 200 };

    public static ServiceResult<T> NaoEncontrado(string mensagem) =>
        new() { Sucesso = false, Mensagem = mensagem, CodigoHttp = 404 };

    public static ServiceResult<T> Falha(string mensagem) =>
        new() { Sucesso = false, Mensagem = mensagem, CodigoHttp = 400 };
}
