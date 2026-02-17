using CamaroteFoliaSync.Application.Commands.RegistrarEntrada;
using CamaroteFoliaSync.Application.Commands.RegistrarSaida;
using CamaroteFoliaSync.Application.Queries.ObterLotacao;
using CamaroteFoliaSync.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CamaroteFoliaSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FluxoController : ControllerBase
{
    private readonly IMediator _mediator;

    public FluxoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("entrada")]
    public async Task<IActionResult> RegistrarEntrada([FromBody] RegistrarEntradaCommand command)
    {
        try
        {
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }
        catch (CapacidadeExcedidaException ex)
        {
            return Conflict(new { erro = ex.Message, lotacalAtual = ex.LotacaoAtual, capacidadeMaxima = ex.CapacidadeMaxima });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost("saida")]
    public async Task<IActionResult> RegistrarSaida([FromBody] RegistrarSaidaCommand command)
    {
        try
        {
            var resultado = await _mediator.Send(command);
            return Ok(resultado);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet("lotacao/{camaroteId:guid}")]
    public async Task<IActionResult> ObterLotacao(Guid camaroteId)
    {
        try
        {
            var query = new ObterLotacaoQuery(camaroteId);
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }
}