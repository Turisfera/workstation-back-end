using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Inquiry.Domain.Models.Commands;
using workstation_back_end.Inquiry.Domain.Models.Queries;
using workstation_back_end.Inquiry.Domain.Services.Services;
using workstation_back_end.Inquiry.Interfaces.REST.Transform;

namespace workstation_back_end.Inquiry.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class InquiryController : ControllerBase
{
    private readonly IInquiryCommandService _commandService;
    private readonly IInquiryQueryService _queryService;

    public InquiryController(IInquiryCommandService commandService, IInquiryQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _queryService.Handle(new GetAllInquiriesQuery());
        return Ok(result.Select(InquiryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("experience/{experienceId:int}")]
    public async Task<IActionResult> GetByExperience(int experienceId)
    {
        var result = await _queryService.Handle(new GetAllInquiriesByExperienceQuery(experienceId));
        return Ok(result.Select(InquiryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInquiryCommand command)
    {
        try
        {
            var inquiry = await _commandService.Handle(command);
            var resource = InquiryResourceFromEntityAssembler.ToResourceFromEntity(inquiry);
            return CreatedAtAction(nameof(GetAll), new { id = resource.Id }, resource);
        }
        catch (FluentValidation.ValidationException e)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = e.Errors.Select(err => new
                {
                    field = err.PropertyName,
                    error = err.ErrorMessage
                })
            });
        }
    }
}