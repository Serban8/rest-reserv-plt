namespace reserv_plt.Core.Dtos
{
    public class ReservationRequestDto
    {
        public Guid TableId { get; internal set; }
        public string CustomerName { get; internal set; }
        public string CustomerEmail { get; internal set; }
        public DateTime DateAndTime { get; internal set; }
    }
}
