using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Enums;

namespace DAL.DB;

public sealed class Context : DbContext
{
    public DbSet<Movie> Movies { get; set;}
    
    public DbSet<Genre> Genres { get; set;}
    
    public DbSet<Director> Directors { get; set;}
    
    public DbSet<MovieGenre> MovieGenres { get; set;}
    
    public DbSet<Comment> Comments { get; set;}
    
    public Context()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var path = Directory.GetCurrentDirectory().Replace("/DAL", "/WebAPI");
        path += "/appsettings.json";
        var configuration = new ConfigurationBuilder().AddJsonFile(path).Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database")!);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasIndex(u => u.Name)
            .IsUnique();
        
        modelBuilder.Entity<Director>()
            .HasIndex(u => u.FullName)
            .IsUnique();
        
        modelBuilder
            .Entity<Genre>()
            .Property(e => e.Id)
            .HasConversion<int>();
        
        modelBuilder
            .Entity<Genre>().HasData(
                Enum.GetValues(typeof(GenreEnum))
                    .Cast<GenreEnum>()
                    .Select(e => new Genre()
                    {
                        Id = e,
                        Name = e.ToString()
                    })
            );
        
    }
    
}