using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Experience.Domain.Models.Commands;
using workstation_back_end.Experience.Domain.Models.Queries;
using workstation_back_end.Experience.Domain.Services;
using workstation_back_end.Experience.Interfaces.REST.Resources;
using workstation_back_end.Experience.Interfaces.REST.Transform;
using FluentValidation;

namespace workstation_back_end.Experience.Interfaces.REST;

/// <summary>
/// API Controller for managing tourism experiences.
/// Supports listing, creating, updating, deleting and filtering experiences.
/// </summary>
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

    /// <summary>
    /// Gets all active experiences in the system.
    /// </summary>
    /// <response code="200">Returns the list of experiences</response>
    /// <response code="404">If no experiences are found</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllExperiencesQuery(); 
        var experiences = await _experienceQueryService.Handle(query);
        var resources = experiences.Select(ExperienceResourceFromEntityAssembler.ToResourceFromEntity);
        return resources.Any() ? Ok(resources) : NotFound("No experiences found.");
    }

    /// <summary>
    /// Creates a new tourism experience.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Experience
    ///     {
    ///        "title": "City Bike Tour",
    ///        "description": "A guided bike tour through downtown.",
    ///        "location": "Lima",
    ///        "duration": 2,
    ///        "price": 35.00,
    ///        "frequencies": "Monday, Wednesday, Friday",
    ///        "rating": 4.5,
    ///        "categoryId": 1,
    ///        "experienceImages": [{ "url": "https://example.com/img.jpg" }],
    ///        "includes": [{ "description": "Bike rental" }],
    ///        "schedules": [{ "time": "09:00" }]
    ///     }
    /// </remarks>
    /// <response code="201">Returns the newly created experience</response>
    /// <response code="400">Validation error or bad request</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateExperienceCommand command)
    {
        try
        {
            var experience = await _experienceCommandService.Handle(command);
            var resource = ExperienceResourceFromEntityAssembler.ToResourceFromEntity(experience);
            return CreatedAtAction(nameof(GetAll), new { id = resource.Id }, resource);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(error => new
            {
                field = error.PropertyName,
                message = error.ErrorMessage
            });

            return BadRequest(new
            {
                message = "Validation failed",
                errors
            });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Updates an existing tourism experience.
    /// </summary>
    /// <param name="id">Experience ID</param>
    /// <param name="command">Update command with new data</param>
    /// <response code="200">Experience updated successfully</response>
    /// <response code="400">Mismatched IDs or invalid request</response>
    /// <response code="404">Experience not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Deletes an experience by its ID.
    /// </summary>
    /// <param name="id">Experience ID</param>
    /// <response code="204">Experience deleted successfully</response>
    /// <response code="404">Experience not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Gets all experiences that belong to a specific category.
    /// </summary>
    /// <param name="categoryId">The ID of the category</param>
    /// <response code="200">Returns the filtered experiences</response>
    /// <response code="404">No experiences found for the category</response>
    [HttpGet("category/{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var query = new GetExperiencesByCategoryQuery(categoryId);
        var experiences = await _experienceQueryService.Handle(query);
        var resources = experiences.Select(ExperienceResourceFromEntityAssembler.ToResourceFromEntity);
        return resources.Any() ? Ok(resources) : NotFound($"No experiences found for category {categoryId}.");
    }
}