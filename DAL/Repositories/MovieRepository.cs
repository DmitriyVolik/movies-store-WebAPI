using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace DAL.Repositories;

internal class MovieRepository : IRepository<Movie, MovieDTO>
{
    private readonly Context _context;
    
    public MovieRepository(Context context)
    {
        _context = context;
    }

    public void AddMovie(MovieDTO movie)
    {
        var newMovie = new Movie
        {
            Id = new Guid(),
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
        };

        var director = _context.Directors
            .FirstOrDefault(x => x.FullName == movie.Director);

        if (director is null)
        {
            throw new Exception("Incorrect director");
        }

        newMovie.Director = director;

        var genres = movie.Genres
            .Select(item => new MovieGenre()
            {
                Genre = _context.Genres.Find(item)
            }).ToList();
        newMovie.Genres = genres;

        _context.Add(newMovie);
    }

    public IEnumerable<Movie> GetMovies()
    {
        return _context.Movies
            .Include(x=>x.Director)
            .Include(x => x.Genres)
            .ThenInclude(x => x.Genre);
    }

    public Movie? GetMovieById(Guid id)
    {
        return _context.Movies
            .Include(x=>x.Director)
            .Include(x => x.Genres)
            .ThenInclude(x => x.Genre)
            .FirstOrDefault(x => x.Id == id);
    }

    public void UpdateMovie(MovieDTO movieUpdate)
    {
        var movie = GetMovieById(movieUpdate.Id);  

        if (movie is null)
        {
            throw new Exception("Incorrect id");
        }
        
        movie.Title = movieUpdate.Title;
        movie.Description = movieUpdate.Description;
        movie.ReleaseDate = movieUpdate.ReleaseDate;
    
        var genres = movieUpdate.Genres
            .Select(item => new MovieGenre()
            {
                Genre = _context.Genres.Find(item)
            }).ToList();
        _context.MovieGenres.RemoveRange(movie.Genres);
        movie.Genres = genres;
    }

    public void DeleteMovie(Guid id)
    {
        var movie = _context.Movies.FirstOrDefault(x => x.Id == id);

        if (movie is null)
        {
            throw new Exception("Incorrect id");
        }
        
        _context.Movies.Remove(movie);
    }
}