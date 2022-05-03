using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
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
    public IEnumerable<CommentModel> Get(Guid movieId)
    {
        return _commentsService.GetCommentsByMovieId(movieId);
    }

    [HttpPost]
    public IActionResult Post(Comment comment)
    {
        _commentsService.AddComment(comment);
        return Ok(comment);
    }
}