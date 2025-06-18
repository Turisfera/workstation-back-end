using workstation_back_end.Booking.Domain.Models;
using workstation_back_end.Booking.Domain.Models.Commands;
using workstation_back_end.Booking.Domain.Services;
using workstation_back_end.Shared.Domain.Repositories;

namespace workstation_back_end.Booking.Application.Internal.CommandServices;

public class BookingCommandService : IBookingCommandService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingCommandService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Models.Entities.Booking> Handle(CreateBookingCommand command)
    {
        var booking = new Domain.Models.Entities.Booking(
            command.ExperienceId,
            command.UserId,
            command.BookingDate,
            command.NumberOfPeople,
            command.Price,
            command.Status
        );

        try
        {
            await _bookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();
            return booking;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Domain.Models.Entities.Booking> Handle(UpdateBookingCommand command)
    {
        var booking = await _bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            throw new ArgumentException($"Booking with ID {command.Id} not found.");

        booking.ExperienceId = command.ExperienceId;
        booking.UserId = command.UserId;
        booking.BookingDate = command.BookingDate;
        booking.NumberOfPeople = command.NumberOfPeople;
        booking.Price = command.Price;
        booking.Status = command.Status;

        try
        {
            _bookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();
            return booking;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> Handle(DeleteBookingCommand command)
    {
        var booking = await _bookingRepository.FindByIdAsync(command.Id);
        if (booking == null) return false;

        try
        {
            _bookingRepository.Remove(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}