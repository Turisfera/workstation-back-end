using Microsoft.AspNetCore.Mvc;
using tripmatch_back.Users.Domain.Models.Commands;
using tripmatch_back.Users.Domain.Models.Entities;
using tripmatch_back.Users.Domain.Models.Queries;
using tripmatch_back.Users.Domain.Services;
using tripmatch_back.Users.Interfaces.REST.Resources;
using tripmatch_back.Users.Interfaces.REST.Transform;

namespace tripmatch_back.Users.Interfaces.REST;

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