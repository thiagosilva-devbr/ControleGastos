using ControleGastos.Dtos;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

/// <summary>Endpoints de gerenciamento de pessoas.</summary> 
[ApiController]
[Route("api/[controller]")]
public class PessoasController(PessoaService pessoaService)
    : ControllerBase
{
    private readonly PessoaService _pessoaService = pessoaService;

    /// <summary>Cadastra uma pessoa.</summary> 
    [HttpPost]
    [ProducesResponseType(
        typeof(PessoaResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PessoaResponse>> Criar(
        [FromBody] CriarPessoaRequest request,
        CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaService.CriarAsync(
            request,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            pessoa);
    }

    /// <summary>Lista todas as pessoas.</summary> 
    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyCollection<PessoaResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<PessoaResponse>>>
        Listar(CancellationToken cancellationToken)
    {
        var pessoas = await _pessoaService.ListarAsync(
            cancellationToken);

        return Ok(pessoas);
    }

    /// <summary> 
    /// Exclui uma pessoa e todas as suas transações. 
    /// </summary> 
    [HttpDelete("{identificador:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(
        int identificador,
        CancellationToken cancellationToken)
    {
        var excluiu = await _pessoaService.ExcluirAsync(
            identificador,
            cancellationToken);

        if (!excluiu)
        {
            return NotFound(new
            {
                mensagem = "Pessoa não encontrada."
            });
        }

        return NoContent();
    }
}