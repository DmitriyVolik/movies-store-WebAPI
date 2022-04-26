using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Models.DTO;

namespace DAL;

public static class ServicesConfiguration
{
    public static void AddDalServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Movie, MovieDTO>, MovieRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}