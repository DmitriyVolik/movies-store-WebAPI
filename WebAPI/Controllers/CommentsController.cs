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
    public IEnumerable<CommentResponseDTO> Get(Guid movieId)
    {
        return _commentsService.GetCommentsByMovieId(movieId);
    }

    [HttpPost]
    public IActionResult Post(CommentRequestDTO commentRequest)
    {
        // try
        // {
             _commentsService.AddComment(commentRequest);
        // }
        // catch (Exception e)
        // {
        //     return BadRequest(e.Message);
        // }
        
        return Ok(commentRequest);
    }

}