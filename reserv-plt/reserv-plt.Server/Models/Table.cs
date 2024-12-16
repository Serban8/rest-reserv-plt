namespace reserv_plt.Server.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; } 
        public bool IsAvailable { get; set; } = true;


    }
}
