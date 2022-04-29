using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.DB;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Context> 
{ 
    public Context CreateDbContext(string[] args)
    {
        var settingsPath = Directory.GetCurrentDirectory().Replace("/DAL", "/WebAPI/appsettings.json");
        IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile(settingsPath).Build();
        var builder = new DbContextOptionsBuilder<Context>();

        builder.UseSqlServer(configuration.GetConnectionString("Database")!); 
        return new Context(builder.Options); 
    } 
}