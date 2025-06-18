using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Reviews.Domain.Models.Commands;
using workstation_back_end.Reviews.Domain.Models.Queries;
using workstation_back_end.Reviews.Domain.Services;
using workstation_back_end.Reviews.Interfaces.REST.Transform;

namespace workstation_back_end.Reviews.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ReviewController : ControllerBase
{
    private readonly IReviewCommandService _reviewCommandService;
    private readonly IReviewQueryService _reviewQueryService;

    public ReviewController(IReviewCommandService reviewCommandService, IReviewQueryService reviewQueryService)
    {
        _reviewCommandService = reviewCommandService;
        _reviewQueryService = reviewQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
    {
        var review = await _reviewCommandService.Handle(command);
        if (review is null) return BadRequest("No se pudo crear la reseña. Verifique si el turista tiene una reserva completada con esta agencia.");
        
        var resource = ReviewAssembler.ToResourceFromEntity(review);
        return StatusCode(201, resource); 
    }

    [HttpGet("agency/{agencyId:int}")]
    public async Task<IActionResult> GetReviewsByAgency(int agencyId)
    {
        var query = new GetReviewsByAgencyIdQuery(agencyId);
        var reviews = await _reviewQueryService.Handle(query);
        var resources = reviews.Select(ReviewAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}