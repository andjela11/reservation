using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : DbContext, IDataContext
{
    public DbSet<Reservation> Reservations { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var reservations = new List<Reservation>();
        for (var i = 0; i < 20; i++)
        {
            var reservation = new Reservation()
            {
                Id = i + 1,
                MovieId = i + 1,
                SeatNumbers = 250
            };
            reservations.Add(reservation);
        }

        modelBuilder.Entity<Reservation>().HasData(reservations);
    }
}
