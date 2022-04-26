using DAL.Entities;
using Models.DTO;

namespace DAL.Repositories.Abstractions;

public interface IMovieRepository
{
    public void AddMovie(MovieDTO movie);
    
    public IEnumerable<Movie> GetMovies();
    
    public Movie? GetMovieById(Guid id);

    public void UpdateMovie(Guid id, MovieDTO movieUpdate);
    
    public void DeleteMovie(Guid id);
}