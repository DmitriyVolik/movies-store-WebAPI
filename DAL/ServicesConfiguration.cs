using DAL.Repositories;
using DAL.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class ServicesConfiguration
{
    public static void AddDalServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}