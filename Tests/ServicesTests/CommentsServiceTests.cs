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
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly Comment _comment;
    
    private readonly Comment _parentComment;

    private readonly CommentsService _service;

    private readonly IList<Comment> _comments;

    private readonly List<Comment> _commentsTree;

    public CommentsServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _comment = A.Dummy<Comment>();
        _comment.Body = "Body";
        _parentComment = A.Dummy<Comment>();
        _service = new CommentsService(_unitOfWork);
        _comments = A.CollectionOfDummy<Comment>(10);
        _commentsTree = new List<Comment>()
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
        A.CallTo(() => _unitOfWork.Comments.GetCommentsByMovieId(Guid.Empty)).Returns(_comments);
        A.CallTo(() => _unitOfWork.Comments.GetById(Guid.Empty)).Returns(_parentComment);
    }
    
    [Fact]
    public void AddComment_ChangeCommentBody_Correct()
    {
        _parentComment.Username = "Name";
        _comment.ParentId = Guid.Empty;

        _service.AddComment(_comment);

        _comment.Body.Should().Contain("["+ _parentComment.Username + "]");
    }
    
    [Fact]
    public void AddComment_IncorrectParentId_IncorrectDataException()
    {
        _comment.ParentId = Guid.Empty;
        A.CallTo(() => _unitOfWork.Comments.GetById(Guid.Empty)).Returns(null);

        var act = () => _service.AddComment(_comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect parentId");
    }
    
    [Fact]
    public void AddComment_IncorrectParentMovieId_IncorrectDataException()
    {
        _comment.MovieId = Guid.NewGuid();
        _comment.ParentId = Guid.Empty;

        var act = () => _service.AddComment(_comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("movie_id is different from parent");
    }

    [Fact]
    public void GetCommentsByMovieId_WithoutTree_CommentModels()
    {
        var result = _service.GetCommentsByMovieId(Guid.Empty);

        result.Count().Should().Be(_comments.Count);
    }
    
    [Fact]
    public void GetCommentsByMovieId_WithTree_CommentModels()
    {
        A.CallTo(() => _unitOfWork.Comments.GetCommentsByMovieId(Guid.Empty)).Returns(_commentsTree);

        var result = _service.GetCommentsByMovieId(Guid.Empty).ToList();
        
        var node = result[3];
        for (var i = 0; i < 4; i++)
        {
            node.Should().NotBeNull();
            node = node!.ParentComment;
        }
    }
}