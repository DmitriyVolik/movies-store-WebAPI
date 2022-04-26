using BLL.Models;
using BLL.Models.DTO;

namespace DAL.Models;

public interface IMovieRepository
{
    public void AddMovie(MovieDTO movie);
    
    public IEnumerable<Movie> GetMovies();
    
    public Movie GetMovieById();

    public void DeleteMovie(int id);
}