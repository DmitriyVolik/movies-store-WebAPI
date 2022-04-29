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
        try
        {
            _commentsService.AddComment(comment);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(comment);
    }
}