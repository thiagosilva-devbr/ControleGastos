using ControleGastos.Dtos;
using ControleGastos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers;

/// <summary>Endpoints de consolidação financeira.</summary> 
[ApiController]
[Route("api/[controller]")]
public class TotaisController(TotaisService totaisService)
    : ControllerBase
{
    private readonly TotaisService _totaisService = totaisService;

    /// <summary> 
    /// Retorna os totais de cada pessoa e o total geral. 
    /// </summary> 
    [HttpGet]
    [ProducesResponseType(
        typeof(ConsultaTotaisResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<ConsultaTotaisResponse>> Consultar(
        CancellationToken cancellationToken)
    {
        var totais = await _totaisService.ConsultarAsync(
            cancellationToken);

        return Ok(totais);
    }
}