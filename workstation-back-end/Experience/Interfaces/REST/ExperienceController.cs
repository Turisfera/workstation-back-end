using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Experience.Domain.Models.Commands;
using workstation_back_end.Experience.Domain.Models.Queries;
using workstation_back_end.Experience.Domain.Services;
using workstation_back_end.Experience.Interfaces.REST.Resources;
using workstation_back_end.Experience.Interfaces.REST.Transform;

namespace workstation_back_end.Experience.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceQueryService _experienceQueryService;
    private readonly IExperienceCommandService _experienceCommandService;

    public ExperienceController(
        IExperienceQueryService experienceQueryService,
        IExperienceCommandService experienceCommandService)
    {
        _experienceQueryService = experienceQueryService;
        _experienceCommandService = experienceCommandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllExperiencesQuery(); 
        var experiences = await _experienceQueryService.Handle(query);
        var resources = experiences.Select(ExperienceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExperienceCommand command)
    {
        var experience = await _experienceCommandService.Handle(command);
        var resource = ExperienceResourceFromEntityAssembler.ToResourceFromEntity(experience);
        return CreatedAtAction(nameof(GetAll), new { id = resource.Id }, resource);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateExperienceCommand command)
    {
        if (id != command.Id)
            return BadRequest("The experience ID in the path and body must match.");

        try
        {
            var experience = await _experienceCommandService.Handle(command);
            var resource = ExperienceResourceFromEntityAssembler.ToResourceFromEntity(experience);
            return Ok(resource);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _experienceCommandService.Handle(new DeleteExperienceCommand(id));
            if (!success) return NotFound($"Experience with ID {id} not found.");
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}