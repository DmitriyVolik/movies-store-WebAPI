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
    private readonly List<User> _users;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly UsersService _service;
    
    private readonly User _user;
    
    public UsersServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _service = new UsersService(_unitOfWork);
        _user = A.Dummy<User>();
        _user.Password = "123";
        _users = new List<User>()
        {
            new User
            {
                Id = Guid.NewGuid(),
                Email = "email1@example.com",
                Name = "Name",
                Password = "Passw0rd%",
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "email2@example.com",
                Name = "Name",
                Password = "Passw0rd%",
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "email3@example.com",
                Name = "Name",
                Password = "Passw0rd%",
            },
        };
        A.CallTo(() => _unitOfWork.Users.Get()).Returns(_users);
        A.CallTo(() => _unitOfWork.Users.GetByEmail("email@example.com")).Returns(_user);
    }
    
    [Fact]
    public void GetUsers_AllUsers_CorrectCount()
    {
        var result = _service.GetUsers();
        
        result.Count().Should().Be(_users.Count);
    }

    [Fact]
    public void AddUser_CorrectPassword_UserModel()
    {
        var expected = _service.UserToModel(_users[0]);

        var result = _service.AddUser(_users[0]);

        result.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void AddUser_IncorrectPassword_IncorrectDataException()
    {
        var act = () => _service.AddUser(_user);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Password must contain at least 8 characters, 1 number and a letter in uppercase");
    }

    [Fact]
    public void GetUserByEmail_User_UserModel()
    {
        var result = _service.GetUserByEmail("email@example.com");

        result.Should().BeEquivalentTo(_user);
    }
}