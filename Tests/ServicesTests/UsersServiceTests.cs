using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Models.Exceptions;
using Xunit;

namespace Tests.ServicesTests;

public class UsersServiceTests
{
    [Fact]
    public void GetUsers_AllUsers_CorrectCount()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var service = new UsersService(unitOfWork);
        A.CallTo(() => unitOfWork.Users.Get()).Returns(_users);
        
        var result = service.GetUsers();
        
        result.Count().Should().Be(_users.Count);
    }

    [Fact]
    public void AddUser_CorrectPassword_UserModel()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var service = new UsersService(unitOfWork);
        var expected = service.UserToModel(_users[0]);

        var result = service.AddUser(_users[0]);

        result.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void AddUser_IncorrectPassword_IncorrectDataException()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var service = new UsersService(unitOfWork);
        var user = A.Dummy<User>();
        user.Password = "123";
        
        var act = () => service.AddUser(user);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Password must contain at least 8 characters, 1 number and a letter in uppercase");
    }

    [Fact]
    public void GetUserByEmail_User_UserModel()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var service = new UsersService(unitOfWork);
        var user = A.Dummy<User>();
        A.CallTo(() => unitOfWork.Users.GetByEmail("email@example.com")).Returns(user);

        var result = service.GetUserByEmail("email@example.com");

        result.Should().BeEquivalentTo(user);
    }

    private readonly List<User> _users = new List<User>()
    {
        new User
        {
            Id = Guid.NewGuid(),
            Email = "email@example.com",
            Name = "Name",
            Password = "Passw0rd%",
        },
        new User
        {
            Id = Guid.NewGuid(),
            Email = "email@example.com",
            Name = "Name",
            Password = "Passw0rd%",
        },
        new User
        {
            Id = Guid.NewGuid(),
            Email = "email@example.com",
            Name = "Name",
            Password = "Passw0rd%",
        },
    };
}