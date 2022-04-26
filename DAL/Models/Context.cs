using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    
    public DbSet<Movie> Movies { get; set;}
    
    public DbSet<Genre> Genres { get; set;}
    
    public DbSet<Genre> Directors { get; set;}
    
    public DbSet<Genre> Comments { get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasIndex(u => u.Name)
            .IsUnique();
        
        modelBuilder.Entity<Director>()
            .HasIndex(u => u.FullName)
            .IsUnique();
        
    }
}