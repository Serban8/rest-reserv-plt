using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>
    {
        public ReservationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
