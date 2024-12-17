using Microsoft.EntityFrameworkCore;
using DataLayer.Models;


namespace DataLayer
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //constraints
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            //relationships
            modelBuilder.Entity<Table>().
                HasMany(t => t.Reservations).
                WithOne(r => r.Table).
                HasForeignKey(r => r.TableID);

            modelBuilder.Entity<User>().
                HasMany(u => u.Reservations).
                WithOne(r => r.User).
                HasForeignKey(r => r.UserID);

            modelBuilder.Entity<User>().
                HasMany(u => u.Feedbacks).
                WithOne(f => f.User).
                HasForeignKey(f => f.UserID);

            modelBuilder.Entity<Reservation>().
                HasOne(r => r.Confirmation).
                WithOne(c => c.Reservation).
                HasForeignKey<Confirmation>(c => c.ReservationID);

            //define auto-inclusions
            modelBuilder.Entity<Reservation>().Navigation(r => r.Table).AutoInclude();
            modelBuilder.Entity<Reservation>().Navigation(r => r.Confirmation).AutoInclude();

            modelBuilder.Entity<User>().Navigation(u => u.Reservations).AutoInclude();
            modelBuilder.Entity<User>().Navigation(u => u.Feedbacks).AutoInclude();
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Confirmation> Confirmations { get; set; }
    }
}
