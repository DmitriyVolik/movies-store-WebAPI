using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.RepositoriesTests;

public class UsersRepositoryTests
{
    [Fact]
    public void Add_CorrectUser_CorrectUserInDb()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("UsersFakeDbForAdd")
            .Options;
        using var context = new Context(options);
        var repository = new UsersRepository(context);
        
        repository.Add(_user);
        context.SaveChanges();

        var user = context.Users.First();
        user.Id.Should().NotBeEmpty();
        user.Role.Should().Be("User");
        user.Password.Should().StartWith("$2a");
    }
    
    [Fact]
    public void Get_AllUsers_Users()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("UsersFakeDbForGet")
            .Options;
        using var context = new Context(options);
        var repository = new UsersRepository(context);
        foreach (var item in _users)
        {
            repository.Add(item);
        }
        context.SaveChanges();

        var result = repository.Get();

        result.Count().Should().Be(_users.Count());
    }
    
    [Fact]
    public void GetByEmail_CorrectEmail_User()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("UsersFakeDbForGetByEmail")
            .Options;
        using var context = new Context(options);
        var repository = new UsersRepository(context);
        repository.Add(_user);
        context.SaveChanges();

        var result = repository.GetByEmail(_user.Email);

        result!.Email.Should().Be(_user.Email);
    }

    private readonly User _user = new User
    {
        Email = "example@gmail.com",
        Name = "Name",
        Password = "Passw0rd%",
    };

    private readonly List<User> _users = new List<User>()
    {
        new User
        {
            Email = "example1@gmail.com",
            Name = "Name",
            Password = "Passw0rd%",
        },
        new User
        {
            Email = "example2@gmail.com",
            Name = "Name",
            Password = "Passw0rd%",
        },
        new User
        {
            Email = "example3@gmail.com",
            Name = "Name",
            Password = "Passw0rd%",
        }
    };
}