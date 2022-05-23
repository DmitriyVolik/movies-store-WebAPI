using System.Runtime.CompilerServices;
using DAL.DB;
using DAL.Repositories;
using DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Tests")]

namespace DAL;

public static class ServicesConfiguration
{
    public static void AddDalServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<Context>
        (options => options.UseSqlServer
            (config.GetConnectionString("Database")!));
    }
}