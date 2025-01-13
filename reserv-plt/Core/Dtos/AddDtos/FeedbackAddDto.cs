using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.AddDtos
{
    public class FeedbackAddDto
    {
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Guid ReservationID { get; set; }
        public Guid UserID { get; set; }
    }
}
