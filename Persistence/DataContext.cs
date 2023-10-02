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
                SeatNumbers = 250,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            reservations.Add(reservation);
        }

        modelBuilder.Entity<Reservation>().HasData(reservations);
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var dateNow = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = dateNow;
            }

            ((BaseEntity)entity.Entity).UpdatedAt = dateNow;
        }
    }
}
