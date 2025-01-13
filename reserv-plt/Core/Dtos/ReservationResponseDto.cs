using DataLayer.Models;

namespace Core.Dtos
{
    public class ReservationResponseDto
    {
        public ReservationResponseDto(Guid reservationId, Guid tableId, int tableNumber, int numberOfPeople, string customerName, DateTime reservationDate, bool isConfirmed)
        {
            ReservationId = reservationId;
            TableId = tableId;
            TableNumber = tableNumber;
            NumberOfPeople = numberOfPeople;
            CustomerName = customerName;
            ReservationDate = reservationDate;
            IsConfirmed = isConfirmed;
        }

        public Guid ReservationId { get; internal set; }
        public Guid TableId { get; internal set; }
        public int TableNumber { get; internal set; }
        public int NumberOfPeople { get; internal set; }
        public string CustomerName { get; internal set; }
        public DateTime ReservationDate { get; internal set; }

        public bool IsConfirmed { get; internal set; }

        public static ReservationResponseDto FromReservation(Reservation reservation)
        {
            return new ReservationResponseDto(reservation.Id, reservation.TableID, reservation.Table.TableNumber, reservation.NumberOfPeople, reservation.User.FirstName, reservation.ReservationDate, reservation.Confirmation.IsConfirmed);
        }
    }
}
