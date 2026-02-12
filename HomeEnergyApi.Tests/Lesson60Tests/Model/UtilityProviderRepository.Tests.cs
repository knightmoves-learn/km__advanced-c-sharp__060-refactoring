using Microsoft.EntityFrameworkCore;
using HomeEnergyApi.Models;

public class UtilityProviderRepositoryTest : IAsyncLifetime
{
    private readonly UtilityProvider _testUtilityProvider;
    private UtilityProviderRepository repository;
    private HomeDbContext _context;

    public UtilityProviderRepositoryTest()
    {
        _testUtilityProvider = new UtilityProvider
        {
            Name = "Test Energy Company",
            ProvidedUtilities = new List<string> { "Electric", "Gas" },
            HomeUtilityProviders = new List<HomeUtilityProvider>()
        };
    }

    public async Task InitializeAsync()
    {
        _context = new MockDb().CreateDbContext();
        repository = new UtilityProviderRepository(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    [Fact]
    public async Task ShouldSaveUtilityProvider_WhenValidUtilityProviderProvided()
    {
        var saveUtilityProvider = repository.Save(_testUtilityProvider);

        Assert.NotNull(saveUtilityProvider);
    }

    // [Fact]
    // public async Task ShouldNotSaveUtilityProvider_WhenInvalidUtilityProviderProvided()
    // {
    //     _testUtilityProvider.Name = null;

    //     var exception = Assert.Throws<DbUpdateException>(() => repository.Save(_testUtilityProvider));

    //     Assert.Contains("Required properties '{'UtilityProvidername'}' are missing for the instance of entity type 'UtilityProvider'.", exception.Message);
    // }

    [Fact]
    public async Task ShouldFindUtilityProviderById_WhenUtilityProviderExists()
    {
        var saveUtilityProvider = repository.Save(_testUtilityProvider);

        var foundUtilityProvider = repository.FindById(saveUtilityProvider.Id);
        Assert.NotNull(foundUtilityProvider);
        Assert.Equal(foundUtilityProvider.Id, saveUtilityProvider.Id);
    }

    [Fact]
    public async Task ShouldNotFindUtilityProviderById_WhenUtilityProviderDoesNotExists()
    {
        var saveUtilityProvider = repository.Save(_testUtilityProvider);

        var foundUtilityProvider = repository.FindById(999);
        Assert.Null(foundUtilityProvider);
    }

    [Fact]
    public async Task ShouldRemoveUtilityProviderById_WhenUtilityProviderExists()
    {
        var saveUtilityProvider = repository.Save(_testUtilityProvider);
        var countBefore = repository.Count();

        var foundUtilityProvider = repository.RemoveById(saveUtilityProvider.Id);

        var countAfter = repository.Count();
        Assert.NotNull(foundUtilityProvider);
        Assert.Equal(foundUtilityProvider.Id, saveUtilityProvider.Id);
        Assert.Equal(1, countBefore);
        Assert.Equal(0, countAfter);
    }

    // [Fact]
    // public async Task ShouldReturnNull_WhenRemovingUtilityProviderWithAnIdThatDoesNotExist()
    // {
    //     var foundUtilityProvider = repository.RemoveById(1);

    //     Assert.Null(foundUtilityProvider);
    // }

    [Fact]
    public async Task ShouldUpdateUtilityProvider_WhenUtilityProviderExists()
    {
        var saveUtilityProvider = repository.Save(_testUtilityProvider);

        saveUtilityProvider.Name = "updatedTestUtilityProvidername";

        repository.Update(saveUtilityProvider.Id, saveUtilityProvider);

        var foundUtilityProvider = repository.FindById(saveUtilityProvider.Id);
        Assert.NotNull(foundUtilityProvider);
        Assert.Equal(foundUtilityProvider.Id, saveUtilityProvider.Id);
        Assert.Equal("updatedTestUtilityProvidername", foundUtilityProvider.Name);
    }

    [Fact]
    public async Task ShouldNotUpdateUtilityProvider_WhenUtilityProviderIsNull()
    {
        Assert.Throws<NullReferenceException>(() => repository.Update(1, null));
    }

    [Fact]
    public async Task ShouldCountUtilityProvider_WhenUtilityProviderExists()
    {
        var saveUtilityProvider = repository.Save(_testUtilityProvider);

        var count = repository.Count();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task ShouldCountUtilityProvider_WhenNoUtilityProvidersExists()
    {
        var count = repository.Count();

        Assert.Equal(0, count);
    }
}