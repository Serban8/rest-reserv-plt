using DataLayer.Models;

namespace Core.Dtos
{
    public class FeedbackDto
    {
        public FeedbackDto(string customerName, string comment, int rating)
        {
            CustomerName = customerName;
            Comment = comment;
            Rating = rating;
        }

        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } = 5;

        public static FeedbackDto FromFeedback(Feedback feedback)
        {
            return new FeedbackDto(feedback.User.FirstName, feedback.Comment, feedback.Rating);
        }
    }
}
