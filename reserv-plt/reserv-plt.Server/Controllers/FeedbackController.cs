using Microsoft.AspNetCore.Mvc;
using Core.Dtos;
using Core.Services;

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
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpPost("submit-feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackDto feedbackDto)
        {
            bool ret = await _feedbackService.Add(feedbackDto);

            if (!ret)
            {
                return BadRequest("Failed to submit feedback.");
            }

            return Ok("Feedback submitted successfully.");
        }

        /// <summary>
        /// MOCK
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-feedback")]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await _feedbackService.GetFeedbacks();

            return Ok(feedbacks);
        }
    }
}
