using HomeEnergyApi.Models;
using Microsoft.EntityFrameworkCore;

public class MockHomeDb : IDbContextFactory<HomeDbContext>
{
    public HomeDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<HomeDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new HomeDbContext(options);
    }
}