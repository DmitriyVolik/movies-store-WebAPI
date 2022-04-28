using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;

namespace DAL.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly Context _context = new Context();

    private MovieRepository _moviesRepository;

    private CommentsRepository _commentsRepository;
    
    private DirectorsRepository _directorsRepository;
    
    private GenresRepository _genresRepository;

    public IRepository<Movie, MovieDTO> Movies
    {
        get
        {
            if (_moviesRepository == null)
            {
                _moviesRepository = new MovieRepository(_context);
            }

            return _moviesRepository;
        }
    }

    public ICommentsRepository Comments
    {
        get
        {
            if (_commentsRepository == null)
            {
                _commentsRepository = new CommentsRepository(_context);
            }

            return _commentsRepository;
        }
    }
    
    public IRepository<Director, Director> Directors
    {
        get
        {
            if (_directorsRepository == null)
            {
                _directorsRepository = new DirectorsRepository(_context);
            }

            return _directorsRepository;
        }
    }
    
    public IRepository<Genre, Genre> Genres
    {
        get
        {
            if (_genresRepository == null)
            {
                _genresRepository = new GenresRepository(_context);
            }

            return _genresRepository;
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