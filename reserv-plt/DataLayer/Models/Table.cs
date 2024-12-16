namespace reserv_plt.DataLayer.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public int TableNumber { get; set; } 
        public bool IsAvailable { get; set; } = true;


    }
}
