using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;
using Models.Exceptions;

namespace BLL.Services;

public class CommentsService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddComment(Comment comment)
    {
        if (comment.ParentId is not null)
        {
            var parentComment = _unitOfWork.Comments.GetById(comment.ParentId);

            if (parentComment is null) throw new IncorrectDataException("Incorrect parentId");

            if (comment.MovieId != parentComment.MovieId) throw new IncorrectDataException("movie_id is different from parent");

            comment.Body = comment.Body.Insert(0, "[" + parentComment.Username + "]");
        }

        _unitOfWork.Comments.Add(comment);
        _unitOfWork.Save();
    }

    public IEnumerable<CommentModel> GetCommentsByMovieId(Guid id)
    {
        var comments = _unitOfWork.Comments.GetCommentsByMovieId(id).ToList();
        
        return comments.Select(item => GetCommentModelTree(item, comments)!).ToList();
    }

    private CommentModel? GetCommentModelTree(Comment? comment, List<Comment> comments)
    {
        if (comment is null)
        {
            return null;
        }

        var commentModel = new CommentModel
        {
            Id = comment.Id,
            Username = comment.Username,
            Body = comment.Body
        };

        if (comment.ParentId is not null)
        {
            commentModel.ParentComment =
                GetCommentModelTree(comments.FirstOrDefault(x => x.Id == comment.ParentId), comments);
        }

        return commentModel;
    }
}