using DAL.Entities;
using Models.DTO;

namespace DAL.Repositories.Abstractions;

public interface ICommentsRepository : IRepository<Comment, CommentDTO>
{
    public IEnumerable<Comment> GetCommentsByMovieId(Guid id);
}