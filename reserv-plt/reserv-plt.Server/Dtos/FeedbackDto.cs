namespace reserv_plt.Server.Dtos
{
    public class FeedbackDto
    (
        string CustomerName,
        string Comment,
        int Rating
    )
    {
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } = 5;
    }
}
