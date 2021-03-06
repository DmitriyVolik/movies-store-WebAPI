using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Exceptions;
using Models.Models;

namespace DAL.Repositories;

internal class MoviesRepository : IRepository<Movie, MovieModel>
{
    private readonly Context _context;

    public MoviesRepository(Context context)
    {
        _context = context;
    }

    public void Add(MovieModel movie)
    {
        var director = _context.Directors.FirstOrDefault(x => x.FullName == movie.Director);
        
        if (director is null) throw new IncorrectDataException("Incorrect director");

        movie.Id = Guid.NewGuid();
        
        var newMovie = new Movie
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            Director = director
        };

        var genres = movie.Genres.Select(item => 
            new MovieGenre
            {
                Genre = _context.Genres.First(x=>x.Name == item.ToString())!
            }).ToList();
        newMovie.Genres = genres;

        _context.Add(newMovie);
    }

    public IEnumerable<Movie> Get()
    {
        return _context.Movies.Include(x => x.Director)
            .Include(x => x.Genres)
            .ThenInclude(x => x.Genre);
    }

    public Movie? GetById(Guid? id)
    {
        return _context.Movies.Include(x => x.Director)
            .Include(x => x.Genres)
            .ThenInclude(x => x.Genre)
            .FirstOrDefault(x => x.Id == id);
    }

    public void Update(MovieModel movieUpdate)
    {
        var movie = GetById(movieUpdate.Id);

        if (movie is null) throw new NotFoundException("Incorrect id");

        movie.Title = movieUpdate.Title;
        movie.Description = movieUpdate.Description;
        movie.ReleaseDate = movieUpdate.ReleaseDate;

        var genres = movie.Genres.Select(item => 
            new MovieGenre
            {
                Genre = _context.Genres.First(x=>x.Name == item.ToString())!
            }).ToList();
        _context.MovieGenres.RemoveRange(movie.Genres);
        movie.Genres = genres;
    }

    public void Delete(Guid id)
    {
        var movie = _context.Movies
            .Include(x=>x.Genres)
            .FirstOrDefault(x => x.Id == id);

        if (movie is null) throw new NotFoundException("Incorrect id");

        _context.Movies.Remove(movie);
    }
}