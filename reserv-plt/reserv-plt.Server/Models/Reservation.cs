namespace reserv_plt.Server.Models
{
    public class Reservation
    {

        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public Table Table { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail{ get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsConfirmed { get; set; } = false;

    }
}
