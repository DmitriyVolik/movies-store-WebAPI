using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly CommentsService _commentsService;

    public CommentsController(CommentsService moviesService)
    {
        _commentsService = moviesService;
    }
    
    [HttpGet("{movieId}")]
    [Authorize("comment:read")]
    public IEnumerable<CommentModel> Get(Guid movieId)
    {
        return _commentsService.GetCommentsByMovieId(movieId);
    }

    [HttpPost]
    [Authorize("comment:write")]
    public IActionResult Post(Comment comment)
    {
        _commentsService.AddComment(comment);
        return Ok(comment);
    }

    [HttpDelete("{commentId}")]
    [Authorize("comment:delete")]
    public void Delete(Guid commentId)
    {
        _commentsService.DeleteComment(commentId);
    }
}