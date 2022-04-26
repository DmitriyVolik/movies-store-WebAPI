using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;

namespace DAL.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;
    
    private IRepository<Movie, MovieDTO> _movieRepository;

    public UnitOfWork(Context context, IRepository<Movie, MovieDTO> movieRepository)
    {
        _context = context;
        _movieRepository = movieRepository;
    }

    public IRepository<Movie, MovieDTO> Movies
    {
        get
        {
            if (this._movieRepository == null)
            {
                this._movieRepository = new MovieRepository(_context);
            }
            return _movieRepository;
        }
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            this._disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

