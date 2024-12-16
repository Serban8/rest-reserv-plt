namespace reserv_plt.Server.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
