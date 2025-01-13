using DataLayer.Models;

namespace Core.Dtos
{
    public class ReservationRequestDto
    {
        public DateTime ReservationDate { get; set; }
        public int NumberOfPeople { get; set; }

        public Guid TableID { get; set; }
        public Guid UserID { get; set; }
    }
}
