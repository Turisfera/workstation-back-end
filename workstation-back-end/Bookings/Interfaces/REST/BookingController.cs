using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Bookings.Domain.Models.Commands;
using workstation_back_end.Bookings.Domain.Models.Queries;
using workstation_back_end.Bookings.Domain.Services;
using workstation_back_end.Bookings.Interfaces.REST.Transform;

namespace workstation_back_end.Bookings.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class BookingController : ControllerBase
{
    private readonly IBookingCommandService _bookingCommandService;
    private readonly IBookingQueryService _bookingQueryService;

    public BookingController(IBookingCommandService bookingCommandService, IBookingQueryService bookingQueryService)
    {
        _bookingCommandService = bookingCommandService;
        _bookingQueryService = bookingQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
    {
        var booking = await _bookingCommandService.Handle(command);
        if (booking is null) return BadRequest("No se pudo crear la reserva.");
        
        var resource = BookingAssembler.ToResourceFromEntity(booking);
        return CreatedAtAction(nameof(GetBookingById), new { bookingId = resource.Id }, resource);
    }

    [HttpGet("{bookingId:int}")]
    public async Task<IActionResult> GetBookingById(int bookingId)
    {
        var query = new GetBookingByIdQuery(bookingId);
        var booking = await _bookingQueryService.Handle(query);
        if (booking is null) return NotFound();
        
        var resource = BookingAssembler.ToResourceFromEntity(booking);
        return Ok(resource);
    }

    [HttpGet("tourist/{touristId:int}")]
    public async Task<IActionResult> GetBookingsByTourist(int touristId)
    {
        var query = new GetBookingsByTouristIdQuery(touristId);
        var bookings = await _bookingQueryService.Handle(query);
        var resources = bookings.Select(BookingAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("agency/{agencyId:int}")]
    public async Task<IActionResult> GetBookingsByAgency(int agencyId)
    {
        var query = new GetBookingsByAgencyIdQuery(agencyId);
        var bookings = await _bookingQueryService.Handle(query);
        var resources = bookings.Select(BookingAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpDelete("{bookingId:int}")]
    public async Task<IActionResult> DeleteBooking(int bookingId)
    {
        var command = new DeleteBookingCommand(bookingId);
        var result = await _bookingCommandService.Handle(command);
        if (!result) return NotFound($"No se encontró la reserva con ID {bookingId}.");
        
        return NoContent(); 
    }
}