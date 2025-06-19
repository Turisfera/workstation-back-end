using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Queries;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Interfaces.REST.Resources;
using workstation_back_end.Users.Interfaces.REST.Transform;

namespace workstation_back_end.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/agencias")]
public class AgenciaController : ControllerBase
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public AgenciaController(IUserCommandService commandService, IUserQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAgencia([FromBody] CreateAgenciaCommand command)
    {
        Agencia agencia = await _commandService.Handle(command);
        var resource = AgenciaResourceAssembler.ToResource(agencia);
        return CreatedAtAction(nameof(GetById), new { userId = resource.UserId }, resource);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var query = new GetUsuarioByIdQuery(userId);
        var usuario = await _queryService.Handle(query);

        if (usuario == null || usuario.Agencia == null)
            return NotFound();

        var resource = AgenciaResourceAssembler.ToResource(usuario.Agencia);
        return Ok(resource);
    }
}