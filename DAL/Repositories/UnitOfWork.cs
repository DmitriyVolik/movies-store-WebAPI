using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;

namespace DAL.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    private MovieRepository _moviesRepository;

    private CommentsRepository _commentsRepository;

    private DirectorsRepository _directorsRepository;

    private GenresRepository _genresRepository;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

    public IRepository<Movie, MovieModel> Movies
    {
        get
        {
            if (_moviesRepository == null) _moviesRepository = new MovieRepository(_context);

            return _moviesRepository;
        }
    }

    public ICommentsRepository Comments
    {
        get
        {
            if (_commentsRepository == null) _commentsRepository = new CommentsRepository(_context);

            return _commentsRepository;
        }
    }

    public IRepository<Director, Director> Directors
    {
        get
        {
            if (_directorsRepository == null) _directorsRepository = new DirectorsRepository(_context);

            return _directorsRepository;
        }
    }

    public IRepository<Genre, Genre> Genres
    {
        get
        {
            if (_genresRepository == null) _genresRepository = new GenresRepository(_context);

            return _genresRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}