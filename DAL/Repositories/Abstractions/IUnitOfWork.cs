using DAL.Entities;
using Models.Models;

namespace DAL.Repositories.Abstractions;

public interface IUnitOfWork
{
    public IRepository<Movie, MovieModel> Movies { get; }
    
    public IRepository<Director, Director> Directors { get; }

    public ICommentsRepository Comments { get; }

    public IUsersRepository Users { get; }

    public void Save();
}