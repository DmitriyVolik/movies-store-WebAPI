using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.RepositoriesTests;

public class DirectorsRepositoryTests
{
    [Fact]
    public void Add_CorrectDirector_DirectorInDb()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("DirectorsFakeDbForAdd")
            .Options;
        using var context = new Context(options);
        var repository = new DirectorsRepository(context);
        
        repository.Add(_director);
        context.SaveChanges();

        context.Directors.First().Should().BeEquivalentTo(_director);
    }

    private readonly Director _director = new Director()
    {
        FullName = "Director"
    };
}