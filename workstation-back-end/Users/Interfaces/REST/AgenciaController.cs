using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Users.Domain.Models.Commands;
using workstation_back_end.Users.Domain.Models.Entities;
using workstation_back_end.Users.Domain.Models.Queries;
using workstation_back_end.Users.Domain.Services;
using workstation_back_end.Users.Interfaces.REST.Resources;
using workstation_back_end.Users.Interfaces.REST.Transform;
using workstation_back_end.Users.Application.CommandServices;
namespace workstation_back_end.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/agencias")]
public class AgenciaController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserCommandService _commandService;

    public AgenciaController(IUserQueryService queryService, IUserCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    /// <summary>
    /// Get agency profile by user ID
    /// </summary>
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var usuario = await _queryService.Handle(new GetUsuarioByIdQuery(userId));
        if (usuario?.Agencia == null) return NotFound();

        var resource = AgenciaResourceAssembler.ToResource(usuario.Agencia);
        return Ok(resource);
    }

    /// <summary>
    /// Update agency profile
    /// </summary>
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateAgenciaCommand command)
    {
        var usuario = await _queryService.Handle(new GetUsuarioByIdQuery(userId));
        if (usuario?.Agencia == null) return NotFound();

        var agencia = usuario.Agencia;

        agencia.AgencyName = command.AgencyName ?? agencia.AgencyName;
        agencia.Ruc = command.Ruc ?? agencia.Ruc;
        agencia.Description = command.Description ?? agencia.Description;
        agencia.AvatarUrl = command.AvatarUrl ?? agencia.AvatarUrl;
        agencia.ContactEmail = command.ContactEmail ?? agencia.ContactEmail;
        agencia.ContactPhone = command.ContactPhone ?? agencia.ContactPhone;
        agencia.SocialLinkFacebook = command.SocialLinkFacebook ?? agencia.SocialLinkFacebook;
        agencia.SocialLinkInstagram = command.SocialLinkInstagram ?? agencia.SocialLinkInstagram;
        agencia.SocialLinkWhatsapp = command.SocialLinkWhatsapp ?? agencia.SocialLinkWhatsapp;

        await _commandService.UpdateAgenciaAsync(userId,command);
        return Ok(AgenciaResourceAssembler.ToResource(agencia));
    }
}