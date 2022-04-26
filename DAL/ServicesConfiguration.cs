using DAL.Models;
using DAL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class ServicesConfiguration
{
    public static void AddDalServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
    }
}