using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class TableRepository : RepositoryBase<Table>
    {
        public TableRepository(AppDbContext context) : base(context)
        {
        }
    }
}
