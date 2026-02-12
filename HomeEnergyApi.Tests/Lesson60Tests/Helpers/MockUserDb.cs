using HomeEnergyApi.Models;
using Microsoft.EntityFrameworkCore;

public class MockUserDb : IDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new UserDbContext(options);
    }
}