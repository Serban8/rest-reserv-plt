namespace reserv_plt.Server.Dtos
{
    public class ReservationRequestDto
    (Guid TableId,
        string CustomerName,
        string CustomerEmail,
        DateTime DateAndTime
    )
    {
        public Guid TableId { get; internal set; }
        public string CustomerName { get; internal set; }
        public string CustomerEmail { get; internal set; }
        public DateTime DateAndTime { get; internal set; }
    }
}
