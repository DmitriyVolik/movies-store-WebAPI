using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Models.Exceptions;
using Xunit;

namespace Tests.RepositoriesTests;

public class CommentsRepositoryTests
{
    [Fact]
    public void Add_CorrectComment_CorrectId()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForAdd")
            .Options;
        using var context = new Context(options);
        context.Movies.Add(Movie);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        repository.Add(_comment);
        context.SaveChanges();

        context.Comments.First().Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Add_IncorrectMovieId_IncorrectDataException()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForAddIncorrect")
            .Options;
        using var context = new Context(options);
        var repository = new CommentsRepository(context);
        
        var act = () => repository.Add(_comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect movie_id");
    }

    [Fact]
    public void GetById_CorrectId_Comment()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForGetById")
            .Options;
        using var context = new Context(options);
        context.Comments.Add(_commentWithId);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        var result = repository.GetById(_commentWithId.Id);

        result!.Id.Should().Be(_commentWithId.Id);
    }
    
    [Fact]
    public void GetCommentsByMovieId_CorrectMovieId_Comments()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForGetByMovieId")
            .Options;
        using var context = new Context(options);
        context.Movies.Add(Movie);
        context.SaveChanges();
        var repository = new CommentsRepository(context);
        repository.Add(_comment);
        context.SaveChanges();
        
        var result = repository.GetCommentsByMovieId(Movie.Id);

        result!.First().MovieId.Should().Be(Movie.Id);
    }

    [Fact]
    public void Delete_CorrectId_NullComment()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForDelete")
            .Options;
        using var context = new Context(options);
        context.Add(_commentWithId);
        context.SaveChanges();
        var repository = new CommentsRepository(context);
        
        repository.Delete(_commentWithId.Id);
        context.SaveChanges();

        context.Comments.FirstOrDefault(x=>x.Id == _commentWithId.Id)
            .Should().BeNull();
    }
    
    [Fact]
    public void Delete_IncorrectId_NotFoundException()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForDelete")
            .Options;
        using var context = new Context(options);
        var repository = new CommentsRepository(context);
        
        var act = () => repository.Delete(_commentWithId.Id);

        act.Should().Throw<NotFoundException>()
            .WithMessage("Id is incorrect");
    }
    
    [Fact]
    public void Delete_CorrectId_SetNullToAllParents()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForDelete")
            .Options;
        using var context = new Context(options);
        var comments = new List<Comment>()
        {
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ParentId = null,
                Body = "Body",
                Username = "Username"
            },
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Body = "Body",
                Username = "Username"
            },
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Body = "Body",
                Username = "Username"
            },
        };
        context.AddRange(comments);
        context.SaveChanges();
        var repository = new CommentsRepository(context);
        
        repository.Delete(comments[0].Id);
        context.SaveChanges();

        foreach (var item in context.Comments)
        {
            item.ParentId.Should().BeNull();
        }
    }
    
    private readonly Comment _commentWithId = new Comment
    {
        Id = Guid.NewGuid(),
        Username = "Username",
        Body = "Body"
    };
    
    private readonly Comment _comment = new Comment
    {
        MovieId = Movie.Id,
        Username = "Username",
        Body = "Body"
    };

    private static readonly Movie Movie = new Movie
    {
        Id = Guid.NewGuid(),
        Title = "Title",
        Description = "Description",
        ReleaseDate = default,
    };
}