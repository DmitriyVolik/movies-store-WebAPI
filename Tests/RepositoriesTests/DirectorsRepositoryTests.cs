using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Tests.RepositoriesTests.Helpers;
using Xunit;

namespace Tests.RepositoriesTests;

public class DirectorsRepositoryTests
{
    private readonly Director _director;

    public DirectorsRepositoryTests()
    {
        _director = new Director
        {
            FullName = "Director"
        };
    }

    [Fact]
    public void Add_CorrectDirector_DirectorInDb()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("DirectorsFakeDbForAdd"));
        var repository = new DirectorsRepository(context);

        repository.Add(_director);
        context.SaveChanges();

        context.Directors.First().Should().BeEquivalentTo(_director);
    }
}