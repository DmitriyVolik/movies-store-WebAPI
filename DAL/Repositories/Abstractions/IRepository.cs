namespace DAL.Repositories.Abstractions;

public interface IRepository<out T, in TDto>
{
    public void AddMovie(TDto movie);
    
    public IEnumerable<T> GetMovies();
    
    public T? GetMovieById(Guid id);

    public void UpdateMovie(TDto movieUpdate);
    
    public void DeleteMovie(Guid id);
}