namespace reserv_plt.Server.Dtos
{
    public class ReservationResponseDto
    (
        Guid ReservationId,
        Guid TableId,
        string CustomerName,
        DateTime ReservationDate,
        bool IsConfirmed
    )
    {
        public Guid ReservationId { get; internal set; }
        public Guid TableId { get; internal set; }

        string CustomerName { get; set; }

        public DateTime ReservationDate { get; internal set; }

        public bool IsConfirmed { get; internal set; }
    }
}
