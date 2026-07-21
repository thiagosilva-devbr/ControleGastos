using ControleGastos.excecoes;
using ControleGastos.excecoes.excecoes;
using System.Net;
using System.Text.Json;

namespace ControleGastos.Middlewares;

/// <summary> 
/// Converte exceções conhecidas em respostas HTTP padronizadas. 
/// </summary> 
public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RecursoNaoEncontradoException exception)
        {
            await EscreverRespostaAsync(
                context,
                HttpStatusCode.NotFound,
                exception.Message);
        }
        catch (RegraNegocioException exception)
        {
            await EscreverRespostaAsync(
                context,
                HttpStatusCode.BadRequest,
                exception.Message);
        }
        catch (ArgumentException exception)
        {
            await EscreverRespostaAsync(
                context,
                HttpStatusCode.BadRequest,
                exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Erro inesperado durante a requisição.");

            await EscreverRespostaAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro inesperado.");
        }
    }

    private static async Task EscreverRespostaAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string mensagem)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(new { mensagem });
        await context.Response.WriteAsync(json);
    }
} 