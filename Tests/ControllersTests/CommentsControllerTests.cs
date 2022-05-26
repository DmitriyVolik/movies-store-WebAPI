using System;
using System.Collections.Generic;
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

public class CommentsControllerTests
{
    private readonly ICommentsService _service;

    private readonly IMapper _mapper;

    private readonly IList<CommentModel> _commentModels;

    private readonly CommentRequestViewModel _commentRequestViewModel;

    private readonly CommentsController _controller;

    public CommentsControllerTests()
    {
        _service = A.Fake<ICommentsService>();
        _mapper = A.Fake<IMapper>();
        _commentModels = A.CollectionOfDummy<CommentModel>(10);
        _commentRequestViewModel = A.Dummy<CommentRequestViewModel>();
        _controller = new CommentsController(_service, _mapper);
        A.CallTo(() => _service.GetCommentsByMovieId(Guid.Empty)).Returns(_commentModels);
    }

    [Fact]
    public void Get_ByMovieId_MovieModels()
    {
        var result = _controller.Get(Guid.Empty);

        result.Count().Should().Be(_commentModels.Count);
    }

    [Fact]
    public void Post_AddComment_Status200()
    {
        var result = _controller.Post(_commentRequestViewModel) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Post_AddComment_Comment()
    {
        var result = _controller.Post(_commentRequestViewModel) as ObjectResult;
        var value = result!.Value;

        value.Should().BeEquivalentTo(_commentRequestViewModel);
    }
}