using DAL.Entities;

namespace DAL.Repositories.Abstractions;

public interface ICommentsRepository : IRepository<Comment, Comment>
{
    public IEnumerable<Comment> GetCommentsByMovieId(Guid id);
}