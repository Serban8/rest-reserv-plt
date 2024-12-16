using Microsoft.EntityFrameworkCore;
using reserv_plt.DataLayer.Models;


namespace reserv_plt.DataLayer
{


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
