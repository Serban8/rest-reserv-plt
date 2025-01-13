using Microsoft.AspNetCore.Mvc;
using Core.Dtos;
using Core.Services;
using Core.Dtos.AddDtos;

namespace reserv_plt.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Add a feedback for a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-feedback")]
        public async Task<IActionResult> Add([FromBody] FeedbackAddDto feedbackDto)
        {
            try
            {
                var feedback = await _feedbackService.Add(feedbackDto);
                return Ok(feedback);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all feedbacks for a restaurant
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-feedback")]
        public async Task<IActionResult> GetFeedbacks(Guid restaurantId)
        {
            var feedbacks = await _feedbackService.GetFeedbacks(restaurantId);

            return Ok(feedbacks);
        }
    }
}
