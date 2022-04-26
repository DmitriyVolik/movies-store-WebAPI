using BLL.Models;
using BLL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    
    public DbSet<Movie> Movies { get; set;}
    
    public DbSet<Genre?> Genres { get; set;}
    
    public DbSet<Director> Directors { get; set;}
    
    public DbSet<MovieGenre> MovieGenres { get; set;}
    
    public DbSet<Comment> Comments { get; set;}

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