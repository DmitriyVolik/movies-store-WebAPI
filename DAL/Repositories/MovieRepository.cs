using BLL.Models;
using BLL.Models.DTO;
using BLL.Models.Enums;
using DAL.Models;

namespace DAL.Services;

internal class MovieRepository : IMovieRepository
{
    private Context _context;
    
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
            Director = _context.Directors.FirstOrDefault(x=>x.FullName == movie.Director)
        };

        var genres = new List<MovieGenre>();
        
        foreach (var item in movie.Genres)
        {
            genres.Add(new MovieGenre()
            {
                Genre = _context.Genres.Find(item)
            });
        }
        
        newMovie.Genres = genres;
        
        _context.Add(newMovie);
        _context.SaveChanges();
    }

    public IEnumerable<Movie> GetMovies()
    {
        throw new NotImplementedException();
    }

    public Movie GetMovieById()
    {
        throw new NotImplementedException();
    }

    public void DeleteMovie(int id)
    {
        throw new NotImplementedException();
    }
}