using System;
using System.Linq;
using AutoMapper;
using BLL.Services.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.Controllers;
using WebAPI.ViewModels;
using Xunit;

namespace Tests.ControllersTests;

public class CommentsTests
{
    [Fact]
    public void Get_Given_MovieId_Return_Comments()
    {
        var service = A.Fake<ICommentsService>();
        var mapper = A.Fake<IMapper>();
        var comments = A.CollectionOfDummy<CommentModel>(10);
        A.CallTo(() => service.GetCommentsByMovieId(Guid.Empty)).Returns(comments);
        var controller = new CommentsController(service, mapper);
        
        var result = controller.Get(Guid.Empty);

        result.Count().Should().Be(comments.Count);
    }

    [Fact]
    public void Post_Given_CommentRequestViewModel_Return_Status200()
    {
        var service = A.Fake<ICommentsService>();
        var comment = A.Fake<CommentRequestViewModel>();
        var mapper = A.Fake<IMapper>();
        var controller = new CommentsController(service, mapper);
        
        var result = controller.Post(comment) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }
}