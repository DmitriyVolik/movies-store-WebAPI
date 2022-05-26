using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Models.Exceptions;
using Tests.RepositoriesTests.Helpers;
using Xunit;

namespace Tests.RepositoriesTests;

public class CommentsRepositoryTests
{
    private readonly Comment _commentWithId;

    private readonly Comment _comment;

    private readonly Movie _movie;

    private readonly List<Comment> _comments;

    public CommentsRepositoryTests()
    {
        _movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Description",
            ReleaseDate = default
        };

        _commentWithId = new Comment
        {
            Id = Guid.NewGuid(),
            Username = "Username",
            Body = "Body"
        };

        _comment = new Comment
        {
            MovieId = _movie.Id,
            Username = "Username",
            Body = "Body"
        };

        _comments = new List<Comment>
        {
            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ParentId = null,
                Body = "Body",
                Username = "Username"
            },
            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Body = "Body",
                Username = "Username"
            },
            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Body = "Body",
                Username = "Username"
            }
        };
    }

    [Fact]
    public void Add_CorrectComment_CorrectId()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForAdd"));
        context.Movies.Add(_movie);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        repository.Add(_comment);
        context.SaveChanges();

        context.Comments.First().Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Add_IncorrectMovieId_IncorrectDataException()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForAddIncorrect"));
        var repository = new CommentsRepository(context);

        var act = () => repository.Add(_comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect movie_id");
    }

    [Fact]
    public void GetById_CorrectId_Comment()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForGetById"));
        context.Comments.Add(_commentWithId);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        var result = repository.GetById(_commentWithId.Id);

        result!.Id.Should().Be(_commentWithId.Id);
    }

    [Fact]
    public void GetCommentsByMovieId_CorrectMovieId_Comments()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForGetByMovieId"));

        context.Movies.Add(_movie);
        context.SaveChanges();
        var repository = new CommentsRepository(context);
        repository.Add(_comment);
        context.SaveChanges();

        var result = repository.GetCommentsByMovieId(_movie.Id);

        result!.First().MovieId.Should().Be(_movie.Id);
    }

    [Fact]
    public void Delete_CorrectId_NullComment()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForDelete"));

        context.Add(_commentWithId);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        repository.Delete(_commentWithId.Id);
        context.SaveChanges();

        context.Comments.FirstOrDefault(x => x.Id == _commentWithId.Id)
            .Should().BeNull();
    }

    [Fact]
    public void Delete_IncorrectId_NotFoundException()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForDelete"));

        var repository = new CommentsRepository(context);

        var act = () => repository.Delete(_commentWithId.Id);

        act.Should().Throw<NotFoundException>()
            .WithMessage("Id is incorrect");
    }

    [Fact]
    public void Delete_CorrectId_SetNullToAllParents()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("CommentsFakeDbForDelete"));

        context.AddRange(_comments);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        repository.Delete(_comments[0].Id);
        context.SaveChanges();

        foreach (var item in context.Comments) item.ParentId.Should().BeNull();
    }
}