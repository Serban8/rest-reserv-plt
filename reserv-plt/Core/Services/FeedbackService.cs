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

namespace Core.Services
{
    public class FeedbackService
    {
        private readonly FeedbackRepository _feedbackRepository;

        public FeedbackService(FeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<bool> Add(FeedbackDto newFeedback)
        {

            //await _feedbackRepository.AddAsync(feedback);

            return true;
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacks()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            var feedbackDtos = feedbacks
                .Select(f => new FeedbackDto(
                    f.User.FirstName,
                    f.Comment,
                    f.Rating));

            return feedbackDtos;
        }
    }
}
