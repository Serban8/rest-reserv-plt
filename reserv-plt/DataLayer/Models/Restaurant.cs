using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Restaurant : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Schedule { get; set; } // JSON-encoded schedule

        public List<Table> Tables { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
