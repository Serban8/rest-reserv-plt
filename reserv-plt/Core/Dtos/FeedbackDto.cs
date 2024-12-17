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
    }
}
