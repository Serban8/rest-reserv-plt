using DataLayer.Models;

namespace DataLayer.Models
{
    public class Table : BaseModel
    {
        public int TableNumber { get; set; } 
        public int Seats { get; set; }
        public string Position { get; set; }

        public Guid RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
