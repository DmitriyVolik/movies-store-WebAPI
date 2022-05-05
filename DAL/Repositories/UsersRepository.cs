using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.Models;

namespace DAL.Repositories;

internal class UsersRepository : IUsersRepository
{
    private readonly Context _context;

    public UsersRepository(Context context)
    {
        _context = context;
    }
    
    public void Add(User user)
    {
        user.Id = new Guid();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Role = "User";
        
        _context.Users.Add(user);
    }

    public IEnumerable<User> Get()
    {
        return _context.Users;
    }

    public User? GetById(Guid? id)
    {
        throw new NotImplementedException();
    }
    
    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(x => x.Email == email);
    }

    public void Update(User update)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}