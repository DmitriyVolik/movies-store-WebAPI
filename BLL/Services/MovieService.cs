using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;
using Models.Enums;

namespace BLL.Services;

public class MovieService
{
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public void AddMovie(MovieDTO movie)
    {
        _unitOfWork.Movies.AddMovie(movie);
        _unitOfWork.Save();
    }

    public IEnumerable<MovieDTO> GetMovies()
    {
        return _unitOfWork.Movies.GetMovies().Select(MovieToDto);
    }

    public MovieDTO? GetMovieById(Guid id)
    {
        var movie = _unitOfWork.Movies.GetMovieById(id);
        return movie is null ? null : MovieToDto(movie);
    }

    public void DeleteMovie(Guid id)
    {
        _unitOfWork.Movies.DeleteMovie(id);
        _unitOfWork.Save();
    }
    
    public void UpdateMovie(Guid id, MovieDTO movie)
    {
        movie.Id = id;
        _unitOfWork.Movies.UpdateMovie(movie);
        _unitOfWork.Save();
    }

    private static MovieDTO MovieToDto(Movie movie)
    {
        var movieDto = new MovieDTO
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
            movieDto.Genres.Add(item.Genre.Id);
        }

        return movieDto;
    }
    
    ~MovieService()
    {
        _unitOfWork.Dispose();
    }
}