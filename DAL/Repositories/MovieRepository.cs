using BLL.Models;
using BLL.Models.DTO;
using DAL.Models;

namespace DAL.Services;

public class MovieRepository : IMovieRepository
{
    private Context _context;
    
    public MovieRepository(Context context)
    {
        _context = context;
    }

    public void AddMovie(MovieDTO movie)
    {
        Console.WriteLine(111);
        var newMovie = new Movie
        {
            Id = new Guid(),
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate
        };
        newMovie.Genres = new List<MovieGenre> {new MovieGenre(){Genre = _context.Genres.First()}};
        
        try
        {
            _context.Add(newMovie);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
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