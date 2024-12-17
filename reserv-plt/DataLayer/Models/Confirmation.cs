using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Confirmation : BaseModel
    {
        public bool IsConfirmed { get; set; } = false;
        
        public Guid ReservationID { get; set; }
        public Reservation Reservation { get; set; }
    }
}
