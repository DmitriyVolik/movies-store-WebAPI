using System.Text.RegularExpressions;
using BLL.Services.Abstractions;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.Exceptions;
using Models.Models;

namespace BLL.Services;

internal class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;

    public UsersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<UserModel> GetUsers()
    {
        return _unitOfWork.Users.Get().Select(UserToModel);
    }

    public UserModel AddUser(User user)
    {
        var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");

        if (!passwordRegex.IsMatch(user.Password))
            throw new IncorrectDataException(
                "Password must contain at least 8 characters, 1 number and a letter in uppercase");

        _unitOfWork.Users.Add(user);
        _unitOfWork.Save();

        return UserToModel(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _unitOfWork.Users.GetByEmail(email);
    }

    public UserModel UserToModel(User user)
    {
        return new UserModel {Id = user.Id, Email = user.Email, Name = user.Name, Role = user.Role};
    }
}