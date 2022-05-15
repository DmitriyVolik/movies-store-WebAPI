using BLL.Services.Abstractions;
using DAL.Entities;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Xunit;

namespace Tests.ControllersTests;

public class DirectorsTests
{
    [Fact]
    public void Add()
    {
        var service = A.Fake<IDirectorsService>();
        var controller = new DirectorsController(service);
        
        var result = controller.Post(_testDirector);
        var okResult = result as OkObjectResult;
        
        Assert.Equal(200, okResult!.StatusCode);
    }
    
    

    private readonly Director _testDirector = new Director
    {
        Id = 1,
        FullName = "Test Director"
    };
}