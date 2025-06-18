using workstation_back_end.Bookings.Domain; 
using workstation_back_end.Reviews.Domain;
using workstation_back_end.Reviews.Domain.Models.Commands;
using workstation_back_end.Reviews.Domain.Models.Entities;
using workstation_back_end.Reviews.Domain.Services;
using workstation_back_end.Shared.Domain.Repositories;

namespace workstation_back_end.Reviews.Application.ReviewCommandService;

public class ReviewCommandService : IReviewCommandService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookingRepository _bookingRepository; 
    private readonly IUnitOfWork _unitOfWork;

    public ReviewCommandService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IBookingRepository bookingRepository)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
        _bookingRepository = bookingRepository;
    }

    public async Task<Review?> Handle(CreateReviewCommand command)
    {
        // Un turista solo puede reseñar una agencia si tiene al menos una reserva completada con ella.
        var touristBookings = await _bookingRepository.FindByTouristIdAsync(command.TouristId);
        var hasCompletedBookingWithAgency = touristBookings.Any(b => 
            b.Experience.UserId == command.AgencyId && b.Status == "Completada");

        if (!hasCompletedBookingWithAgency)
        {
            Console.WriteLine("Error: El turista no tiene una reserva completada con esta agencia.");
            return null; 
        }
        
        var review = new Review(
            command.TouristId,
            command.AgencyId,
            command.Rating,
            command.Comment
        );

        try
        {
            await _reviewRepository.AddAsync(review);
            await _unitOfWork.CompleteAsync();
            return review;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the review: {e.Message}");
            return null;
        }
    }
}