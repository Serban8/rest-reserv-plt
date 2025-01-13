using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.AddDtos
{
    public class TableAddDto
    {
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public string Position { get; set; }

        public Guid RestaurantID { get; set; }
    }
}
