using DAL.Entities;
using Models.Models;

namespace BLL.Services.Abstractions;

public interface IUsersService
{
    public IEnumerable<UserModel> GetUsers();

    public UserModel AddUser(User user);

    public User? GetUserByEmail(string email);

    public UserModel UserToModel(User user);
}