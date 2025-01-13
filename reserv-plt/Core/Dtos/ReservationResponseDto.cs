namespace Core.Dtos
{
    public class ReservationResponseDto
    {
        public ReservationResponseDto(Guid reservationId, Guid tableId, int tableNumber, string customerName, DateTime reservationDate, bool isConfirmed)
        {
            ReservationId = reservationId;
            TableId = tableId;
            TableNumber = tableNumber;
            CustomerName = customerName;
            ReservationDate = reservationDate;
            IsConfirmed = isConfirmed;
        }

        public Guid ReservationId { get; internal set; }
        public Guid TableId { get; internal set; }
        public int TableNumber { get; internal set; }
        public string CustomerName { get; internal set; }
        public DateTime ReservationDate { get; internal set; }

        public bool IsConfirmed { get; internal set; }
    }
}
