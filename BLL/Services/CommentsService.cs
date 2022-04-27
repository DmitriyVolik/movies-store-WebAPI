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
    
    public void AddComment(CommentDTO comment)
    {
        if (comment.ParentId != Guid.Empty)
        {
            var parentComment = GetCommentById(comment.ParentId);
        
            if (parentComment is null)
            {
                throw new Exception("Incorrect parentId");
            }
            
            comment.Body = comment.Body
                .Insert(0, "[" + parentComment.Username + "]");
        }

        _unitOfWork.Comments.Add(comment);
        _unitOfWork.Save();
    }

    public IEnumerable<CommentMinDTO> GetCommentsByMovieId(Guid id)
    {
        var comments = _unitOfWork.Comments.GetCommentsByMovieId(id);

         var commentsMinDto = new List<CommentMinDTO>();
        
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

    private CommentDTO? GetCommentById(Guid id)
    {
        var comment = _unitOfWork.Comments.GetById(id);
        return comment is null ? null : CommentToDto(comment);
    }

    private static CommentDTO CommentToDto(Comment comment)
    {
        return new CommentDTO()
        {
            Id = comment.Id,
            ParentId = comment.ParentId,
            MovieId = comment.Movie.Id,
            Username = comment.Username,
            Body = comment.Body
        };
    }
    
    private static CommentMinDTO CommentToMinDto(Comment comment)
    {
        return new CommentMinDTO()
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