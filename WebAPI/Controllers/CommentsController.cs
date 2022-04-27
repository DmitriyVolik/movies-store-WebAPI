using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using WebApiTasks.ActionFilters;

namespace WebApiTasks.Controllers;

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
    public IEnumerable<CommentMinDTO> Get(Guid movieId)
    {
        return _commentsService.GetCommentsByMovieId(movieId);
    }

    [HttpPost]
    public IActionResult Post(CommentDTO comment)
    {
        // try
        // {
             _commentsService.AddComment(comment);
        // }
        // catch (Exception e)
        // {
        //     return BadRequest(e.Message);
        // }
        
        return Ok(comment);
    }

}