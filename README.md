# 060 Refactoring

## Lecture

[![# Refactoring](https://img.youtube.com/vi/DDPE-EU6MEk/0.jpg)](https://www.youtube.com/watch?v=DDPE-EU6MEk)


## Instructions

**Important:** Do not modify any test files inside `HomeEnergyApi.Tests/GradingTests/` as these are used to grade your assignment.

When you run tests for this assignment, you will see four failing tests until you fully complete the assignment. If you see more than four tests failing, you can consider that as tests failing as a result of your refactoring.

Begin the assignment by running `dotnet test` and ensuring you have only four failing tests. Run `dotnet test` throughout the assignment to ensure you are completing the refactoring steps correctly.

Before starting this lesson two new files at `HomeEnergyApi/Models/UserDbContext.cs` and `HomeEnergyApi/Models/UtilityProviderDbContext.cs` have been created for you.

- Begin by copying the code from `HomeEnergyApi/Models/HomeDbContext.cs` to `UserDbContext.cs` and `UtilityProviderDbContext.cs`, and rename the class, constructor and type passed to `DbContextOptions<T>` to eliminate any errors.
- In `HomeEnergyApi/Program.cs`
  - Copy the statement beginning on line 48 to add `HomeDbContext` to `builder.Services` twice, changing the given type on `AddDbContext<T>()` to add `UserDbContext` and `UtilityProviderDbContext` to the builder's services as well
    - You will notice the argument passed to `UseSqlite()` may look different than it has in the past and different from the video example, there is also a `ConfigureWarnings()` call added to ignore the warning `NonTransactionalMigrationOperationWarning`. You should copy these as well as they are required for your lesson environment

In `HomeEnergyApi/Models/UserRepository.cs`
  - Change `HomeDbContext` to `UserDbContext` anywhere it is used
  - Run `dotnet test`
    - You should have one more error...  `error CS1503: Argument 1:` at the file `UserRepository.Tests.cs`

Rename the file `HomeEnergyApi.Tests/Lesson60Tests/Helpers/MockDb.cs` to `MockHomeDb.cs`
  - Be sure to change the name of the class in the file, as well as renaming the file itself

You will also see two new files at `HomeEnergyApi.Tests/Lesson60Tests/Helpers/MockUserDb.cs` and `HomeEnergyApi.Tests/Lesson60Tests/Helpers/MockUtilityProviderDb.cs` have been created for you
  - Fill in these files, by copying the code from `MockHomeDb.cs`, replacing "Home" accordingly with either "User" or "UtilityProvider" depending on the file.
    - The name of the class, the given type for `IDbContextFactory<T>`, the return type on `CreateDbContext()` and the given type for `DbContextOptionsBuilder<T>` should all use the correct name or type for the given file

In `HomeEnergyApi.Tests/Lesson60Tests/Model/HomeRepository.Tests.cs`
  - Update `_context` to instead use the `MockHomeDb` and ensure _context is of type `HomeDbContext`

In `HomeEnergyApi.Tests/Lesson60Tests/Model/UserRepository.Tests.cs`
  - Update `_context` to instead use the `MockUserDb` and ensure _context is of type `UserDbContext`

In `HomeEnergyApi.Tests/Lesson60Tests/Model/UtilityProviderRepository.Tests.cs`
  - Update `_context` to instead use the `MockUtilityProviderDb` and ensure _context is of type `UtilityProviderDbContext`

At this point if you run `dotnet test` all tests should be green

## Additional Information

- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:
- Develop and use a series of test cases to verify that a program performs according to its design specifications (3B-AP-21) https://www.csteachers.org/page/standards

## Resources
- https://en.wikipedia.org/wiki/Unit_testing
- https://martinfowler.com/bliki/TestPyramid.html
- https://xunit.net/
- https://en.wikipedia.org/wiki/Code_refactoring


Copyright &copy; 2025 Knight Moves. All Rights Reserved.
