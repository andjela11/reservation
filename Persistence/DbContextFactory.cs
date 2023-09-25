using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence;

public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseMySQL("Server=localhost;Port=3306;User ID=root;Password=root123;Database=reservations");

        return new DataContext(dbContextBuilder.Options);
    }
}
