using workstation_back_end.Bookings.Domain;
using workstation_back_end.Bookings.Domain.Models.Commands;
using workstation_back_end.Bookings.Domain.Models.Entities;
using workstation_back_end.Bookings.Domain.Services;
using workstation_back_end.Shared.Domain.Repositories;

namespace workstation_back_end.Bookings.Application.BookingCommandService;

public class BookingCommandService : IBookingCommandService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingCommandService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Booking?> Handle(CreateBookingCommand command)
    {
        var booking = new Booking(
            command.TouristId,
            command.ExperienceId,
            command.BookingDate,
            command.NumberOfPeople,
            command.Price,
            "Confirmada" 
        );

        try
        {
            await _bookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();
            return booking;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the booking: {e.Message}");
            return null;
        }
    }

    public async Task<Booking?> Handle(UpdateBookingStatusCommand command)
    {
        var existingBooking = await _bookingRepository.FindByIdAsync(command.BookingId);
        if (existingBooking is null) return null; 

        existingBooking.Status = command.NewStatus;

        try
        {
            _bookingRepository.Update(existingBooking);
            await _unitOfWork.CompleteAsync();
            return existingBooking;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the booking status: {e.Message}");
            return null;
        }
    }

    public async Task<bool> Handle(DeleteBookingCommand command)
    {
        var existingBooking = await _bookingRepository.FindByIdAsync(command.BookingId);
        if (existingBooking is null) return false; 

        try
        {
            _bookingRepository.Remove(existingBooking);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the booking: {e.Message}");
            return false;
        }
    }
}