using System.Linq;
using BLL.Services.Abstractions;
using FakeItEasy;
using Models.Models;
using WebAPI.Controllers;
using Xunit;

namespace Tests.ControllersTests;

public class MoviesTests
{
    [Fact]
    public void Get()
    {
        var service = A.Fake<IMoviesService>();
        var movies = A.CollectionOfFake<MovieModel>(10);
        A.CallTo(() => service.GetMovies()).Returns(movies);
        var controller = new MoviesController(service);
        
        var result = controller.Get();
        
        Assert.Equal(movies.Count,result.Count());
    }
}