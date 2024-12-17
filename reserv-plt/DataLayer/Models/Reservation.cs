using DataLayer.Models;

namespace DataLayer.Models
{
    public class Reservation : BaseModel
    {
        public DateTime ReservationDate { get; set; }
        public int NumberOfPeople { get; set; }

        public Guid TableID { get; set; }
        public Table Table { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public Confirmation Confirmation { get; set; } = new Confirmation();
    }
}
