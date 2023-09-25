using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IDataContext
{
    public DbSet<Reservation> Reservations { get; set; }
    
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken token);
    
    Task<int> SaveChangesAsync(bool test, CancellationToken token);
}
