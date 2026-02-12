public class FileTests
{
    private static string programFilePath = @"../../../../HomeEnergyApi/Program.cs";
    private static string homeRepositoryTestPath = @"../../../Lesson60Tests/Model/HomeRepository.Tests.cs";
    private static string userRepositoryTestPath = @"../../../Lesson60Tests/Model/UserRepository.Tests.cs";
    private static string utilityProviderRepositoryTestPath = @"../../../Lesson60Tests/Model/UtilityProviderRepository.Tests.cs";
    private string programContent = File.ReadAllText(programFilePath);
    private string homeRepositoryTestContent = File.ReadAllText(homeRepositoryTestPath);
    private string userRepositoryTestContent = File.ReadAllText(userRepositoryTestPath);
    private string utilityProviderRepositoryTestContent = File.ReadAllText(utilityProviderRepositoryTestPath);

    [Fact]
    public void DoesProgramAddUserandUtilityPRovidertoDbContext()
    {
        bool containsUserRepositoryDbContext = programContent.Contains("builder.Services.AddDbContext<UserDbContext>(");
        Assert.True(containsUserRepositoryDbContext,
            "HomeEnergyApi/Program.cs does not add a Scoped Service of type `UserDbContext`");

        bool containsUtilityProviderDbContext = programContent.Contains("builder.Services.AddDbContext<UtilityProviderDbContext>(");
        Assert.True(containsUtilityProviderDbContext,
            "HomeEnergyApi/Program.cs does not add a Scoped Service of type `UtilityProviderDbContext`");
    }

    [Fact]
    public void DoesHomeRepositoryTestUseMockHomeDb()
    {
        bool containsMockHomeDb = homeRepositoryTestContent.Contains("MockHomeDb()");
        Assert.True(containsMockHomeDb,
            "HomeEnergyApi.Tests/Lesson60Tests/Model/HomeRepository.Tests.cs should use MockHomeDb()");
    }

    [Fact]
    public void DoesHomeRepositoryTestUseMockUserDb()
    {
        bool containsMockUserDb = userRepositoryTestContent.Contains("MockUserDb()");
        Assert.True(containsMockUserDb,
            "HomeEnergyApi.Tests/Lesson60Tests/Model/HomeRepository.Tests.cs should use MockUserDb()");
    }

    [Fact]
    public void DoesHomeRepositoryTestUseMockUtilityProviderDb()
    {
        bool containsMockUtilityProviderDb = utilityProviderRepositoryTestContent.Contains("MockUtilityProviderDb()");
        Assert.True(containsMockUtilityProviderDb,
            "HomeEnergyApi.Tests/Lesson60Tests/Model/HomeRepository.Tests.cs should use MockUtilityProviderDb()");
    }
}
