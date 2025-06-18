using workstation_back_end.Booking.Domain.Models.Commands;

namespace workstation_back_end.Booking.Domain.Services;

public interface IBookingCommandService
{
    Task<Models.Entities.Booking> Handle(CreateBookingCommand command);
    Task<Models.Entities.Booking> Handle(UpdateBookingCommand command);
    Task<bool> Handle(DeleteBookingCommand command);
}