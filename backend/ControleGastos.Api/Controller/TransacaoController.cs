using ControleGastos.Dtos;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

/// <summary>Endpoints de cadastro e listagem de transações.</summary> 
[ApiController]
[Route("api/[controller]")]
public class TransacoesController(
    TransacaoService transacaoService)
    : ControllerBase
{
    private readonly TransacaoService _transacaoService =
        transacaoService;

    /// <summary>Cadastra uma receita ou despesa.</summary> 
    [HttpPost]
    [ProducesResponseType(
        typeof(TransacaoResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransacaoResponse>> Criar(
        [FromBody] CriarTransacaoRequest request,
        CancellationToken cancellationToken)
    {
        var transacao = await _transacaoService.CriarAsync(
            request,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            transacao);
    }

    /// <summary>Lista todas as transações.</summary> 
    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<TransacaoResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TransacaoResponse>>>
        Listar(CancellationToken cancellationToken)
    {
        var transacoes = await _transacaoService.ListarAsync(
            cancellationToken);

        return Ok(transacoes);
    }
}