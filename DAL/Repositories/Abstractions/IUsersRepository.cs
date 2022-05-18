using DAL.Entities;
using Models.Models;

namespace DAL.Repositories.Abstractions;

public interface IUsersRepository : IRepository<User, User>
{
    public User? GetByEmail(string email);
}