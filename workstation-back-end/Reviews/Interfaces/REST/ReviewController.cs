using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Reviews.Domain.Models.Commands;
using workstation_back_end.Reviews.Domain.Models.Queries;
using workstation_back_end.Reviews.Domain.Services;
using workstation_back_end.Reviews.Interfaces.REST.Transform;

namespace workstation_back_end.Reviews.Interfaces.REST
{
    /// <summary>
    /// API Controller para la gestión de reseñas.
    /// Permite crear y consultar reseñas asociadas a agencias.
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
        /// Crea una nueva reseña para una reserva completada.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/Review
        ///     {
        ///        "touristId": 123,
        ///        "agencyId": 45,
        ///        "rating": 4.7,
        ///        "comment": "Excelente servicio y guía muy amable."
        ///     }
        /// </remarks>
        /// <response code="201">Devuelve la reseña recién creada</response>
        /// <response code="400">Si no se pudo crear la reseña (por ejemplo, sin reserva completada)</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var review = await _reviewCommandService.Handle(command);
            if (review is null)
                return BadRequest("No se pudo crear la reseña. Verifique si el turista tiene una reserva completada con esta agencia.");

            var resource = ReviewAssembler.ToResourceFromEntity(review);
            return StatusCode(201, resource);
        }

        /// <summary>
        /// Obtiene todas las reseñas registradas para una agencia.
        /// </summary>
        /// <param name="agencyId">ID de la agencia</param>
        /// <response code="200">Devuelve la lista de reseñas de la agencia</response>
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
