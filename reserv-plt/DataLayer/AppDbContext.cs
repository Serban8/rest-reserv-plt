using Microsoft.EntityFrameworkCore;
using reserv_plt.DataLayer.Models;


namespace reserv_plt.DataLayer
{


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // configure the context to use SQLlite and log to the console, but only warning level logs
            optionsBuilder
                    .UseSqlite($"Data Source=./local-db/reserv-plt.db")
                    //.UseMySQL(applicationBuilder.Configuration.GetConnectionString("MySQLConnection"), b => b.MigrationsAssembly("reserv-plt.Server")
                    .LogTo(System.Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Warning);
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
