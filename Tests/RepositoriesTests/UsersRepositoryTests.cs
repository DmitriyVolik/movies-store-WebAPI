using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Tests.RepositoriesTests.Helpers;
using Xunit;

namespace Tests.RepositoriesTests;

public class UsersRepositoryTests
{
    private readonly User _user;

    private readonly List<User> _users;

    public UsersRepositoryTests()
    {
        _user = new User
        {
            Email = "example@gmail.com",
            Name = "Name",
            Password = "Passw0rd%"
        };

        _users = new List<User>
        {
            new()
            {
                Email = "example1@gmail.com",
                Name = "Name",
                Password = "Passw0rd%"
            },
            new()
            {
                Email = "example2@gmail.com",
                Name = "Name",
                Password = "Passw0rd%"
            },
            new()
            {
                Email = "example3@gmail.com",
                Name = "Name",
                Password = "Passw0rd%"
            }
        };
    }

    [Fact]
    public void Add_CorrectUser_UserWithCorrectId()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("UsersFakeDbForAdd"));
        var repository = new UsersRepository(context);

        repository.Add(_user);
        context.SaveChanges();

        var user = context.Users.First();
        user.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Add_CorrectUser_UserWithCorrectRole()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("UsersFakeDbForAdd"));
        var repository = new UsersRepository(context);

        repository.Add(_user);
        context.SaveChanges();

        var user = context.Users.First();
        user.Role.Should().Be("User");
    }

    [Fact]
    public void Add_CorrectUser_UserPasswordHash()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("UsersFakeDbForAdd"));
        var repository = new UsersRepository(context);

        repository.Add(_user);
        context.SaveChanges();

        var user = context.Users.First();
        user.Password.Should().StartWith("$2a");
    }

    [Fact]
    public void Get_AllUsers_Users()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("UsersFakeDbForGet"));
        var repository = new UsersRepository(context);
        foreach (var item in _users) repository.Add(item);
        context.SaveChanges();

        var result = repository.Get();

        result.Count().Should().Be(_users.Count());
    }

    [Fact]
    public void GetByEmail_CorrectEmail_User()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("UsersFakeDbForGetByEmail"));
        var repository = new UsersRepository(context);
        repository.Add(_user);
        context.SaveChanges();

        var result = repository.GetByEmail(_user.Email);

        result!.Email.Should().Be(_user.Email);
    }
}