using Microsoft.AspNetCore.Mvc;
using tripmatch_back.Users.Domain.Models.Commands;
using tripmatch_back.Users.Domain.Models.Queries;
using tripmatch_back.Users.Domain.Services;
using tripmatch_back.Users.Interfaces.REST.Resources;
using tripmatch_back.Users.Interfaces.REST.Transform;

namespace tripmatch_back.Users.Interfaces.REST;


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