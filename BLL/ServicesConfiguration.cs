using BLL.Services;
using BLL.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class ServicesConfiguration
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IDirectorsService, DirectorsService>();
        services.AddScoped<IMoviesService, MoviesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}