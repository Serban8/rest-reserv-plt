using DataLayer.Models;

namespace DataLayer.Models
{
    public class Feedback : BaseModel
    {
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Guid ReservationID { get; set; }
        public Reservation Reservation { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
