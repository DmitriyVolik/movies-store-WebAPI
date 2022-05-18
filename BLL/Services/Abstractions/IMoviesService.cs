using DAL.Entities;
using Models.Models;

namespace BLL.Services.Abstractions;

public interface IMoviesService
{
    public void AddMovie(MovieModel movie);

    public IEnumerable<MovieModel> GetMovies();

    public MovieModel? GetMovieById(Guid id);

    public void DeleteMovie(Guid id);

    public void UpdateMovie(Guid id, MovieModel movie);

    public MovieModel MovieToModel(Movie movie);
}