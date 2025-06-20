using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Reviews.Domain.Models.Commands;
using workstation_back_end.Reviews.Domain.Models.Queries;
using workstation_back_end.Reviews.Domain.Services;
using workstation_back_end.Reviews.Interfaces.REST.Transform;

namespace workstation_back_end.Reviews.Interfaces.REST
{
    /// <summary>
    /// API Controller for managing reviews.
    /// It allows creating and querying reviews associated with agencies.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewCommandService _reviewCommandService;
        private readonly IReviewQueryService _reviewQueryService;

        public ReviewController(
            IReviewCommandService reviewCommandService,
            IReviewQueryService reviewQueryService)
        {
            _reviewCommandService = reviewCommandService;
            _reviewQueryService = reviewQueryService;
        }

        /// <summary>
        /// Creates a new review for a completed booking.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/Review
        ///     {
        ///        "touristId": 123,
        ///        "agencyId": 45,
        ///        "rating": 4.7,
        ///        "comment": "Excellent service and very friendly guide."
        ///     }
        /// </remarks>
        /// <param name="command">The command object for creating the review.</param>
        /// <response code="201">Returns the newly created review.</response>
        /// <response code="400">If the review could not be created (e.g., no completed booking exists).</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var review = await _reviewCommandService.Handle(command);
            if (review is null)
                return BadRequest("Could not create the review. Verify if the tourist has a completed booking with this agency.");

            var resource = ReviewAssembler.ToResourceFromEntity(review);
            return StatusCode(201, resource);
        }

        /// <summary>
        /// Gets all registered reviews for a specific agency.
        /// </summary>
        /// <param name="agencyId">The agency's ID.</param>
        /// <response code="200">Returns the list of reviews for the agency.</response>
        [HttpGet("agency/{agencyId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviewsByAgency(int agencyId)
        {
            var query = new GetReviewsByAgencyIdQuery(agencyId);
            var reviews = await _reviewQueryService.Handle(query);
            var resources = reviews.Select(ReviewAssembler.ToResourceFromEntity);
            return Ok(resources);
        }
    }
}