using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;

namespace DAL.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly Context _context = new Context();
    
    private MovieRepository _movieRepository;

    public IRepository<Movie, MovieDTO> Movies
    {
        get
        {
            if (_movieRepository == null)
            {
                _movieRepository = new MovieRepository(_context);
            }
            return _movieRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}