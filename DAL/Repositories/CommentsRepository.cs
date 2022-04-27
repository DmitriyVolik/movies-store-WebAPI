using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace DAL.Repositories;

public class CommentsRepository : ICommentsRepository
{
    private readonly Context _context;
    
    public CommentsRepository(Context context)
    {
        _context = context;
    }
    public void Add(CommentRequestDTO commentRequest)
    {
        var movie = _context.Movies.FirstOrDefault(x => x.Id == commentRequest.MovieId);
        if (movie is null)
        {
            throw new Exception("Incorrect movieId");
        }

        var newComment = new Comment
        {
            Id = Guid.NewGuid(),
            ParentId = commentRequest.ParentId,
            Username = commentRequest.Username,
            Body = commentRequest.Body,
            Movie = movie
        };

        commentRequest.Id = newComment.Id;
        
        _context.Comments.Add(newComment);
    }

    public IEnumerable<Comment> Get()
    {
        return _context.Comments
            .Include(x => x.Movie);
    }

    public Comment? GetById(Guid? id)
    {
        return _context.Comments
            .Include(x=>x.Movie)
            .Include(x=>x.Parent)
            .ThenInclude(x=>x.Parent)
            .FirstOrDefault(x => x.Id == id);
    }
    
    public IEnumerable<Comment> GetCommentsByMovieId(Guid id)
    {
        return _context.Comments
            .Include(x=>x.Movie)
            .Include(x=>x.SubComments)
            .Where(x=>x.Movie.Id == id);
    }

    public void Update(CommentRequestDTO commentRequestUpdate)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}