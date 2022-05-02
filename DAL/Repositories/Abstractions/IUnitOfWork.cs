using DAL.Entities;
using Models.DTO;

namespace DAL.Repositories.Abstractions;

public interface IUnitOfWork
{
    public IRepository<Movie, MovieModel> Movies { get; }
    
    public IRepository<Director, Director> Directors { get; }

    public ICommentsRepository Comments { get; }

    public void Save();
}