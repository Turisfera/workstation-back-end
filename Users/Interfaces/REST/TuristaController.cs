using Microsoft.AspNetCore.Mvc;
using tripmatch_back.Users.Domain.Models.Commands;
using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Domain.Models.Queries;
using tripmatch_back.Users.Domain.Services;
using tripmatch_back.Users.Interfaces.REST.Resources;
using tripmatch_back.Users.Interfaces.REST.Transform;

namespace tripmatch_back.Users.Interfaces.REST;

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