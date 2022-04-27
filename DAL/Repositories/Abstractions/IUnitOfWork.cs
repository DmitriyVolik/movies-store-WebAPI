using DAL.Entities;
using Models.DTO;

namespace DAL.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    public IRepository<Movie, MovieDTO> Movies { get; }
    
    public ICommentsRepository Comments { get; }

    public void Save();
}