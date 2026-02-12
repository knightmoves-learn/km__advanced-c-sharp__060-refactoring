using HomeEnergyApi.Models;
using Microsoft.EntityFrameworkCore;

public class MockUtilityProviderDb : IDbContextFactory<UtilityProviderDbContext>
{
    public UtilityProviderDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<UtilityProviderDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new UtilityProviderDbContext(options);
    }
}