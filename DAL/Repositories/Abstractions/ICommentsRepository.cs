using DAL.Entities;
using Models.DTO;

namespace DAL.Repositories.Abstractions;

public interface ICommentsRepository : IRepository<Comment, CommentRequestDTO>
{
    public IEnumerable<Comment> GetCommentsByMovieId(Guid id);
}