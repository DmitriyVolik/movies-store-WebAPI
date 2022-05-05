using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.Exceptions;

namespace DAL.Repositories;

internal class CommentsRepository : ICommentsRepository
{
    private readonly Context _context;

    public CommentsRepository(Context context)
    {
        _context = context;
    }

    public void Add(Comment comment)
    {
        var movie = _context.Movies.FirstOrDefault(x => x.Id == comment.MovieId);
        if (movie is null) throw new IncorrectDataException("Incorrect movie_id");

        comment.Id = Guid.NewGuid();

        _context.Comments.Add(comment);
    }

    public IEnumerable<Comment> Get()
    {
        return _context.Comments;
    }

    public Comment? GetById(Guid? id)
    {
        return _context.Comments.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Comment> GetCommentsByMovieId(Guid id)
    {
        return _context.Comments.Where(x => x.MovieId == id);
    }

    public void Update(Comment commentRequestUpdate)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        var comment = GetById(id);
        
        if (comment is null) throw new NotFoundException("Id is incorrect");

        foreach (var item in _context.Comments.Where(x => x.ParentId == comment.Id))
        {
            item.ParentId = null;
        }

        _context.Comments.Remove(comment);
    }
}