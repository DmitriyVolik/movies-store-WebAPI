using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Models.Exceptions;
using Xunit;

namespace Tests.ServicesTests;

public class CommentsServiceTests
{
    [Fact]
    public void AddComment_ChangeCommentBody_Correct()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var comment = A.Dummy<Comment>();
        var parentComment = A.Dummy<Comment>();
        parentComment.Username = "Name";
        comment.ParentId = Guid.Empty;
        comment.Body = "Body";
        A.CallTo(() => unitOfWork.Comments.GetById(Guid.Empty)).Returns(parentComment);
        var service = new CommentsService(unitOfWork);
        
        service.AddComment(comment);

        comment.Body.Should().Contain("["+ parentComment.Username + "]");
    }
    
    [Fact]
    public void AddComment_IncorrectParentId_IncorrectDataException()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var comment = A.Dummy<Comment>();
        comment.ParentId = Guid.Empty;
        comment.Body = "Body";
        A.CallTo(() => unitOfWork.Comments.GetById(Guid.Empty)).Returns(null);
        var service = new CommentsService(unitOfWork);
        
        var act = () => service.AddComment(comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect parentId");
    }
    
    [Fact]
    public void AddComment_IncorrectParentMovieId_IncorrectDataException()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var comment = A.Dummy<Comment>();
        var parentComment = A.Dummy<Comment>();
        comment.MovieId = Guid.NewGuid();
        comment.ParentId = Guid.Empty;
        comment.Body = "Body";
        A.CallTo(() => unitOfWork.Comments.GetById(Guid.Empty)).Returns(parentComment);
        var service = new CommentsService(unitOfWork);
        
        var act = () => service.AddComment(comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("movie_id is different from parent");
    }

    [Fact]
    public void GetCommentsByMovieId_WithoutTree_CommentModels()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var comments = A.CollectionOfDummy<Comment>(10);
        A.CallTo(() => unitOfWork.Comments.GetCommentsByMovieId(Guid.Empty)).Returns(comments);
        var service = new CommentsService(unitOfWork);

        var result = service.GetCommentsByMovieId(Guid.Empty);

        result.Count().Should().Be(comments.Count);
    }
    
    [Fact]
    public void GetCommentsByMovieId_WithTree_CommentModels()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        var comments = new List<Comment>()
        {
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ParentId = null,
            },
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },
            new Comment
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                ParentId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            },
        };
        A.CallTo(() => unitOfWork.Comments.GetCommentsByMovieId(Guid.Empty)).Returns(comments);
        var service = new CommentsService(unitOfWork);

        var result = service.GetCommentsByMovieId(Guid.Empty).ToList();
        var node = result[3];
        for (var i = 0; i < 4; i++)
        {
            node.Should().NotBeNull();
            node = node!.ParentComment;
        }
    }
}