﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Enums;

namespace DAL.DB;

public sealed class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    
    public DbSet<Movie> Movies { get; set;}
    
    public DbSet<Genre> Genres { get; set;}
    
    public DbSet<Director> Directors { get; set;}
    
    public DbSet<MovieGenre> MovieGenres { get; set;}
    
    public DbSet<Comment> Comments { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();
        
        modelBuilder.Entity<Director>()
            .HasIndex(d => d.FullName)
            .IsUnique();

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(x => x.Genres)
            .OnDelete(DeleteBehavior.Cascade);
            

        modelBuilder
            .Entity<Genre>().HasData(
                Enum.GetValues(typeof(GenreEnum))
                    .Cast<GenreEnum>()
                    .Select(e => new Genre()
                    {
                        Id = Convert.ToInt32(e),
                        Name = e.ToString()
                    })
            );
    }
}