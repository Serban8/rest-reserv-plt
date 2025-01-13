using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Core.Dtos;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;
using Core.Dtos.AddDtos;

namespace Core.Services
{
    public class FeedbackService
    {
        private readonly FeedbackRepository _feedbackRepository;
        private readonly ReservationRepository _reservationRepository;

        public FeedbackService(FeedbackRepository feedbackRepository, ReservationRepository reservationRepository)
        {
            _feedbackRepository = feedbackRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<FeedbackDto> Add(FeedbackAddDto newFeedback)
        {
            var reservation = await _reservationRepository.GetByIdAsync(newFeedback.ReservationID) ?? throw new Exception("Reservation not found");
            if (reservation.Feedback != null)
            {
                throw new Exception("Feedback already exists");
            }

            if (newFeedback.Rating < 1 || newFeedback.Rating > 5)
            {
                throw new Exception("Rating must be between 1 and 5");
            }

            if (reservation.UserID != newFeedback.UserID)
            {
                throw new Exception("User does not have permission to add feedback for this reservation");
            }

            if (reservation.IsFinished == false)
            {
                throw new Exception("Feedback can only be added for completed reservations");
            }

            try
            {
                var feedback = new Feedback
                {
                    Comment = newFeedback.Comment,
                    Rating = newFeedback.Rating,
                    UserID = newFeedback.UserID,
                    ReservationID = newFeedback.ReservationID
                };

                feedback = await _feedbackRepository.AddAsync(feedback) ?? throw new Exception("Failed to add feedback");
                await _feedbackRepository.SaveAllChangesAsync();
                return FeedbackDto.FromFeedback(feedback);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add feedback", ex);
            }
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacks(Guid restaurantId)
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            
            var feedbackDtos = feedbacks.Where(f => f.Reservation.RestaurantId == restaurantId)
                .Select(f => FeedbackDto.FromFeedback(f)).ToList();
            return feedbackDtos;
        }
    }
}
