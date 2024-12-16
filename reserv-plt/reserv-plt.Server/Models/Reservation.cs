namespace reserv_plt.Server.Models
{
    public class Reservation
    {

        public int Id { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail{ get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsConfirmed { get; set; } = false;

    }
}
