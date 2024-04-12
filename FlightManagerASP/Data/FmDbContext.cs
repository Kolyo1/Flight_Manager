using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class FmDbContext : IdentityDbContext<dbUser, IdentityRole, string>
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        public FmDbContext()
        {
            
        }

        public FmDbContext(DbContextOptions<FmDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FlightManagerDB;Integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Reservation>()
                .HasOne(f => f.Flight)
                .WithMany(r => r.Reservations);
            builder.Entity<Flight>()
                .HasMany(r => r.Reservations)
                .WithOne(f => f.Flight)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
