using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Bookings.Domain.Models.Commands;
using workstation_back_end.Bookings.Domain.Models.Queries;
using workstation_back_end.Bookings.Domain.Services;
using workstation_back_end.Bookings.Interfaces.REST.Transform;

namespace workstation_back_end.Bookings.Interfaces.REST
{
    /// <summary>
    /// API Controller para la gestión de reservas.
    /// Permite crear, consultar y eliminar reservas de turistas y agencias.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class BookingController : ControllerBase
    {
        private readonly IBookingCommandService _bookingCommandService;
        private readonly IBookingQueryService _bookingQueryService;

        public BookingController(
            IBookingCommandService bookingCommandService,
            IBookingQueryService bookingQueryService)
        {
            _bookingCommandService = bookingCommandService;
            _bookingQueryService = bookingQueryService;
        }

        /// <summary>
        /// Crea una nueva reserva.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/Booking
        ///     {
        ///        "touristId": 123,
        ///        "agencyId": 45,
        ///        "experienceId": 67,
        ///        "startDate": "2025-07-01T09:00:00",
        ///        "endDate": "2025-07-01T12:00:00",
        ///        "participants": 4
        ///     }
        /// </remarks>
        /// <response code="201">Devuelve la reserva recién creada</response>
        /// <response code="400">Error de validación o datos inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var booking = await _bookingCommandService.Handle(command);
            if (booking is null)
                return BadRequest("No se pudo crear la reserva.");

            var resource = BookingAssembler.ToResourceFromEntity(booking);
            return CreatedAtAction(nameof(GetBookingById), new { bookingId = resource.Id }, resource);
        }

        /// <summary>
        /// Obtiene una reserva por su ID.
        /// </summary>
        /// <param name="bookingId">ID de la reserva</param>
        /// <response code="200">Devuelve la reserva solicitada</response>
        /// <response code="404">Si no existe la reserva con ese ID</response>
        [HttpGet("{bookingId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingById(int bookingId)
        {
            var query = new GetBookingByIdQuery(bookingId);
            var booking = await _bookingQueryService.Handle(query);
            if (booking is null)
                return NotFound($"No se encontró la reserva con ID {bookingId}.");

            var resource = BookingAssembler.ToResourceFromEntity(booking);
            return Ok(resource);
        }

        /// <summary>
        /// Obtiene todas las reservas de un turista específico.
        /// </summary>
        /// <param name="touristId">ID del turista</param>
        /// <response code="200">Devuelve la lista de reservas del turista</response>
        [HttpGet("tourist/{touristId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingsByTourist(int touristId)
        {
            var query = new GetBookingsByTouristIdQuery(touristId);
            var bookings = await _bookingQueryService.Handle(query);
            var resources = bookings.Select(BookingAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        /// <summary>
        /// Obtiene todas las reservas de una agencia específica.
        /// </summary>
        /// <param name="agencyId">ID de la agencia</param>
        /// <response code="200">Devuelve la lista de reservas de la agencia</response>
        [HttpGet("agency/{agencyId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingsByAgency(int agencyId)
        {
            var query = new GetBookingsByAgencyIdQuery(agencyId);
            var bookings = await _bookingQueryService.Handle(query);
            var resources = bookings.Select(BookingAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        /// <summary>
        /// Elimina una reserva por su ID.
        /// </summary>
        /// <param name="bookingId">ID de la reserva</param>
        /// <response code="204">Reserva eliminada correctamente</response>
        /// <response code="404">Si no existe la reserva con ese ID</response>
        [HttpDelete("{bookingId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var command = new DeleteBookingCommand(bookingId);
            var result = await _bookingCommandService.Handle(command);
            if (!result)
                return NotFound($"No se encontró la reserva con ID {bookingId}.");

            return NoContent();
        }
    }
}
