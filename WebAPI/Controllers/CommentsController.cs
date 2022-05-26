using AutoMapper;
using BLL.Services.Abstractions;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.ActionFilters;
using WebAPI.ViewModels;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;
    
    private readonly IMapper _mapper;

    public CommentsController(ICommentsService commentsService, IMapper mapper)
    {
        _commentsService = commentsService;
        _mapper = mapper;
    }
    
    [HttpGet("{movieId}")]
    [Authorize("comment:read")]
    public IEnumerable<CommentModel> Get(Guid movieId)
    {
        return _commentsService.GetCommentsByMovieId(movieId);
    }

    [HttpPost]
    [Authorize("comment:write")]
    public IActionResult Post(CommentRequestViewModel commentRequest)
    {
        _commentsService.AddComment(_mapper.Map<Comment>(commentRequest));
        return Ok(commentRequest);
    }

    [HttpDelete("{commentId}")]
    [Authorize("comment:delete")]
    public void Delete(Guid commentId)
    {
        _commentsService.DeleteComment(commentId);
    }
}