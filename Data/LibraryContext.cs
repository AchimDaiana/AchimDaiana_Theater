using AchimDaiana_Theater.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace AchimDaiana_Theater.Data
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<TheaterPlay> TheaterPlays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Play>().ToTable("Play");
            modelBuilder.Entity<Writer>().ToTable("Writer");
            modelBuilder.Entity<Theater>().ToTable("Theater");
            modelBuilder.Entity<TheaterPlay>().ToTable("TheaterPlay");
            modelBuilder.Entity<TheaterPlay>()
            .HasKey(c => new { c.PlayID, c.TheaterID });
        }
    }
}
