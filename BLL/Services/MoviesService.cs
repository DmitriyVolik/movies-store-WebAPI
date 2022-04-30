using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;
using Models.Enums;

namespace BLL.Services;

public class MoviesService
{
    private readonly IUnitOfWork _unitOfWork;

    public MoviesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddMovie(MovieModel movie)
    {
        _unitOfWork.Movies.Add(movie);
        _unitOfWork.Save();
    }

    public IEnumerable<MovieModel> GetMovies()
    {
        return _unitOfWork.Movies.Get().Select(MovieToModel);
    }

    public MovieModel? GetMovieById(Guid id)
    {
        var movie = _unitOfWork.Movies.GetById(id);
        return movie is null ? null : MovieToModel(movie);
    }

    public void DeleteMovie(Guid id)
    {
        _unitOfWork.Movies.Delete(id);
        _unitOfWork.Save();
    }

    public void UpdateMovie(Guid id, MovieModel movie)
    {
        movie.Id = id;
        _unitOfWork.Movies.Update(movie);
        _unitOfWork.Save();
    }

    private static MovieModel MovieToModel(Movie movie)
    {
        var movieModel = new MovieModel
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            Director = movie.Director.FullName,
            Genres = new List<GenreEnum>()
        };

        foreach (var item in movie.Genres)
        {
            movieModel.Genres.Add(item.Genre.Id);
        }
        
        return movieModel;
    }
}