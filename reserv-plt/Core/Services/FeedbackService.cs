using Microsoft.EntityFrameworkCore;
using reserv_plt.Core.Dtos;
using reserv_plt.DataLayer;
using reserv_plt.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FeedbackService
    {
        private readonly AppDbContext _context;

        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(FeedbackDto newFeedback)
        {
            var feedback = new Feedback
            {
                CustomerName = newFeedback.CustomerName,
                Comment = newFeedback.Comment,
                Rating = newFeedback.Rating
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Select(f => new FeedbackDto(
                    f.CustomerName,
                    f.Comment,
                    f.Rating))
                .ToListAsync();

            return feedbacks;
        }
    }
}
