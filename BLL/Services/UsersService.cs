using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.Models;

namespace BLL.Services;

public class UsersService
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
        _unitOfWork.Users.Add(user);
        _unitOfWork.Save();

        return UserToModel(user);
    }
    
    public User? GetByEmail(string email)
    {
        return _unitOfWork.Users.GetByEmail(email);
    }

    public UserModel UserToModel(User user)
    {
        return new UserModel
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
    }
}