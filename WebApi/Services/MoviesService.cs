using BLL.Models;
using DAL.Models;

namespace WebApiTasks.Services;

public class MoviesService
{
    private static readonly List<Movie> _movies;

    static MoviesService()
    {
        _movies = new List<Movie>();
    }

    public IEnumerable<Movie> GetAllMovies()
    {
        return _movies;
    }

    public Movie? GetMovieById(Guid id)
    {
        //return _movies.FirstOrDefault(x => x.Id == id);
        return null;
    }

    public void AddMovie(Movie movie)
    {
        //movie.Id = Guid.NewGuid();
        _movies.Add(movie);
    }

    public void DeleteMovie(Guid id)
    {
        //_movies.Remove(_movies.First(x => x.Id == id));
    }

    public void UpdateMovie(Guid id, Movie movieUpdate)
    {
        //var movie = _movies.First(x => x.Id == id);

        // movie.Title = movieUpdate.Title;
        // movie.Description = movieUpdate.Description;
        // movie.Genres = movieUpdate.Genres;
        // movie.ReleaseDate = movieUpdate.ReleaseDate;
    }
}