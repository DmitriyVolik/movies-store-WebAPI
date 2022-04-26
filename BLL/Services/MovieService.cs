using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;
using Models.Enums;

namespace BLL.Services;

public class MovieService
{
    private readonly IMovieRepository _moviesRepository;

    public MovieService(IMovieRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }
    
    public void AddMovie(MovieDTO movie)
    {
        _moviesRepository.AddMovie(movie);
    }

    public IEnumerable<MovieDTO> GetMovies()
    {
        return _moviesRepository.GetMovies().Select(MovieToDto);
    }

    public MovieDTO? GetMovieById(Guid id)
    {
        var movie = _moviesRepository.GetMovieById(id);
        return movie is null ? null : MovieToDto(movie);
    }

    public void DeleteMovie(Guid id)
    {
        _moviesRepository.DeleteMovie(id);
    }
    
    public void UpdateMovie(Guid id, MovieDTO movie)
    {
        _moviesRepository.UpdateMovie(id, movie);
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
}