namespace Core.Dtos
{
    public class ReservationResponseDto
    {
        private Guid id1;
        private Guid id2;

        public ReservationResponseDto(Guid id1, Guid id2, string customerName, DateTime reservationDate, bool isConfirmed)
        {
            this.id1 = id1;
            this.id2 = id2;
            CustomerName = customerName;
            ReservationDate = reservationDate;
            IsConfirmed = isConfirmed;
        }

        public Guid ReservationId { get; internal set; }
        public Guid TableId { get; internal set; }

        string CustomerName { get; set; }

        public DateTime ReservationDate { get; internal set; }

        public bool IsConfirmed { get; internal set; }
    }
}
