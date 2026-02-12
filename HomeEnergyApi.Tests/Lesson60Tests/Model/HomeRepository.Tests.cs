using Microsoft.EntityFrameworkCore;
using HomeEnergyApi.Models;

public class HomeRepositoryTest : IAsyncLifetime
{
    private readonly Home _testHome;
    private HomeRepository repository;
    private HomeDbContext _context;

    public HomeRepositoryTest()
    {
        _testHome = new Home(
            ownerLastName: "TestOwner",
            streetAddress: "123 Test Street",
            city: "Test City"
        )
        {
            HomeUtilityProviders = new List<HomeUtilityProvider>()
        };
    }

    public async Task InitializeAsync()
    {
        _context = new MockDb().CreateDbContext();
        repository = new HomeRepository(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    [Fact]
    public async Task ShouldSaveHome_WhenValidHomeProvided()
    {
        var saveHome = repository.Save(_testHome);

        Assert.NotNull(saveHome);
        Assert.Equal(1, saveHome.Id);
    }

    [Fact]
    public async Task ShouldNotSaveHome_WhenInvalidHomeProvided()
    {
        _testHome.OwnerLastName = null;

        var exception = Assert.Throws<DbUpdateException>(() => repository.Save(_testHome));

        Assert.Contains("Required properties '{'OwnerLastName'}' are missing for the instance of entity type 'Home'.", exception.Message);
    }

    [Fact]
    public async Task ShouldFindHomeById_WhenHomeExists()
    {
        var saveHome = repository.Save(_testHome);

        var foundHome = repository.FindById(saveHome.Id);
        Assert.NotNull(foundHome);
        Assert.Equal(foundHome.Id, saveHome.Id);
    }

    [Fact]
    public async Task ShouldNotFindHomeById_WhenHomeDoesNotExists()
    {
        var saveHome = repository.Save(_testHome);

        var foundHome = repository.FindById(999);

        Assert.Null(foundHome);
    }

    [Fact]
    public async Task ShouldRemoveHomeById_WhenHomeExists()
    {
        var saveHome = repository.Save(_testHome);
        var countBefore = repository.Count();

        var foundHome = repository.RemoveById(saveHome.Id);

        var countAfter = repository.Count();
        Assert.NotNull(foundHome);
        Assert.Equal(foundHome.Id, saveHome.Id);
        Assert.Equal(1, countBefore);
        Assert.Equal(0, countAfter);
    }

    [Fact]
    public async Task ShouldUpdateHome_WhenHomeExists()
    {
        var saveHome = repository.Save(_testHome);

        saveHome.OwnerLastName = "updatedTestName";

        repository.Update(saveHome.Id, saveHome);

        var foundHome = repository.FindById(saveHome.Id);
        Assert.NotNull(foundHome);
        Assert.Equal(foundHome.Id, saveHome.Id);
        Assert.Equal("updatedTestName", foundHome.OwnerLastName);
    }

    [Fact]
    public async Task ShouldNotUpdateHome_WhenHomeIsNull()
    {
        Assert.Throws<NullReferenceException>(() => repository.Update(1, null));
    }

    [Fact]
    public async Task ShouldCountHome_WhenHomeExists()
    {
        var saveHome = repository.Save(_testHome);

        var count = repository.Count();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task ShouldCountHome_WhenNoHomesExists()
    {
        var count = repository.Count();

        Assert.Equal(0, count);
    }
}