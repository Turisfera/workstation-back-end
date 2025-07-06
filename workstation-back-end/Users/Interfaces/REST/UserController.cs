using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Queries;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Interfaces.REST.Resources;
using workstation_back_end.Users.Interfaces.REST.Transform;

namespace workstation_back_end.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserCommandService _commandService;

    public UserController(IUserQueryService queryService, IUserCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    /// <summary>
    /// Get a user by user ID
    /// </summary>
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var user = await _queryService.Handle(new GetUserByIdQuery(userId));
        if (user == null) return NotFound();

        var resource = UserResourceFromEntityAssembler.ToResource(user);
        return Ok(resource);
    }

    /// <summary>
    /// Soft delete a user by ID (mark as inactive)
    /// </summary>
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        await _commandService.DeleteUserAsync(userId);
        return NoContent();
    }
}