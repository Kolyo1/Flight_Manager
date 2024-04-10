using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FmDbContext : IdentityDbContext<dbUser>
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<dbUser> Users { get; set; }

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
