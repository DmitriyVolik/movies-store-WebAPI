using DAL.Entities;
using Models.Exceptions;
using Models.Models;

namespace BLL.Services.Abstractions;

public interface ICommentsService
{
    public void AddComment(Comment comment);

    public IEnumerable<CommentModel> GetCommentsByMovieId(Guid id);

    public void DeleteComment(Guid id);
}