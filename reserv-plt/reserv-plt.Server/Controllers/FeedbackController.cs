using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reserv_plt.Server.Data;
using reserv_plt.Server.Dtos;
using reserv_plt.Server.Models;

namespace reserv_plt.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackDto feedbackDto)
        {
            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                CustomerName = feedbackDto.CustomerName,
                Comment = feedbackDto.Comment,
                Rating = feedbackDto.Rating,
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok("Feedback submitted successfully.");
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Select(f => new FeedbackDto(
                    f.CustomerName,
                    f.Comment,
                    f.Rating
                ))
                .ToListAsync();

            return Ok(feedbacks);
        }
    }
}
