using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RestaurantRepository : RepositoryBase<Restaurant>
    {
        public RestaurantRepository(AppDbContext context) : base(context)
        {
        }
        
    }
}
