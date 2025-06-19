using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Queries;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Interfaces.REST.Resources;
using workstation_back_end.Users.Interfaces.REST.Transform;

namespace workstation_back_end.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/turistas")]
public class TuristaController : ControllerBase
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public TuristaController(IUserCommandService commandService, IUserQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTurista([FromBody] CreateTuristaCommand command)
    {
        Turista turista = await _commandService.Handle(command);
        var resource = TuristaResourceAssembler.ToResource(turista);
        return CreatedAtAction(nameof(GetById), new { userId = resource.UserId }, resource);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var query = new GetUsuarioByIdQuery(userId);
        var usuario = await _queryService.Handle(query);

        if (usuario == null || usuario.Turista == null)
            return NotFound();

        var resource = TuristaResourceAssembler.ToResource(usuario.Turista);
        return Ok(resource);
    }
}