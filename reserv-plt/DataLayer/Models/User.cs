using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        //TODO Level is dependent on the number of reviews a user has made - this should be automatically calculated and is not set in the database
        [NotMapped]
        public string Level { get; set; }


        public List<Feedback> Feedbacks { get; set; }
        public List<Reservation> Reservations { get; set; }

    }
}
