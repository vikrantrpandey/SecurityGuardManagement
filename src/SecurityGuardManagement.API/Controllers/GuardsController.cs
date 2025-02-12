using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecurityGuardManagement.Application.DTOs;
using SecurityGuardManagement.Application.Guards.Commands.CreateGuard;
using SecurityGuardManagement.Application.Guards.Commands.DeleteGuard;
using SecurityGuardManagement.Application.Guards.Commands.UpdateGuard;
using SecurityGuardManagement.Application.Guards.Queries.GetGuardById;
using SecurityGuardManagement.Application.Guards.Queries.GetGuardsList;

namespace SecurityGuardManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GuardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GuardDto>> GetById(int id)
    {
        var guard = await _mediator.Send(new GetGuardByIdQuery(id));
        
        if (guard == null)
            return NotFound();

        return Ok(guard);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GuardDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GuardDto>> Create([FromBody] CreateGuardCommand command)
    {
        var guard = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = guard.Id }, guard);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GuardDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GuardDto>>> GetAll()
    {
        var guards = await _mediator.Send(new GetGuardsListQuery());
        return Ok(guards);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGuardCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteGuardCommand(id));
        return NoContent();
    }
}
