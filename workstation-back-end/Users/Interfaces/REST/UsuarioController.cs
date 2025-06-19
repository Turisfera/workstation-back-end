using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Queries;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Interfaces.REST.Resources;
using workstation_back_end.Users.Interfaces.REST.Transform;

namespace workstation_back_end.Users.Interfaces.REST;


[ApiController]
[Route("api/v1/users")]
public class UsuarioController : ControllerBase
{
    private readonly IUserCommandService _commandService;
    private readonly IUserQueryService _queryService;

    public UsuarioController(IUserCommandService commandService, IUserQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioCommand command)
    {
        var usuario = await _commandService.Handle(command);
        var resource = UsuarioResourceFromEntityAssembler.ToResource(usuario);
        return CreatedAtAction(nameof(GetById), new { userId = usuario.UserId }, resource);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var query = new GetUsuarioByIdQuery(userId);
        var usuario = await _queryService.Handle(query);
        if (usuario == null)
            return NotFound();

        var resource = UsuarioResourceFromEntityAssembler.ToResource(usuario);
        return Ok(resource);
    }
}