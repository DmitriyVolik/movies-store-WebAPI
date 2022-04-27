using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;

namespace BLL.Services;

public class CommentsService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public void AddComment(CommentRequestDTO commentRequest)
    {
        if (commentRequest.ParentId != Guid.Empty)
        {
            var parentComment = GetCommentById(commentRequest.ParentId);
        
            if (parentComment is null)
            {
                throw new Exception("Incorrect parentId");
            }
            
            commentRequest.Body = commentRequest.Body
                .Insert(0, "[" + parentComment.Username + "]");
        }

        _unitOfWork.Comments.Add(commentRequest);
        _unitOfWork.Save();
    }

    public IEnumerable<CommentResponseDTO> GetCommentsByMovieId(Guid id)
    {
        var comments = _unitOfWork.Comments.GetCommentsByMovieId(id);

         var commentsMinDto = new List<CommentResponseDTO>();
        
         foreach (var item in comments)
         {
             var comment = CommentToMinDto(item);
             
             var parent = _unitOfWork.Comments.GetById(item.ParentId);
             
             if (parent is not null)
             {
                 comment.ParentComment = CommentToMinDto(parent);
             }
             
             commentsMinDto.Add(comment);
         }
        
         return commentsMinDto;
    }

    private CommentRequestDTO? GetCommentById(Guid id)
    {
        var comment = _unitOfWork.Comments.GetById(id);
        return comment is null ? null : CommentToDto(comment);
    }

    private static CommentRequestDTO CommentToDto(Comment comment)
    {
        return new CommentRequestDTO()
        {
            Id = comment.Id,
            ParentId = comment.ParentId,
            MovieId = comment.Movie.Id,
            Username = comment.Username,
            Body = comment.Body
        };
    }
    
    private static CommentResponseDTO CommentToMinDto(Comment comment)
    {
        return new CommentResponseDTO()
        {
            Id = comment.Id,
            Username = comment.Username,
            Body = comment.Body,
        };
    }
    
    ~CommentsService()
    {
        _unitOfWork.Dispose();
    }
}