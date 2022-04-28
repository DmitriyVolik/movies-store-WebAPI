using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;

namespace DAL.Repositories;

public class GenresRepository : IRepository<Genre, Genre>
{
    private readonly Context _context;

    public GenresRepository(Context context)
    {
        _context = context;
    }

    public void Add(Genre genre)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Genre> Get()
    {
        throw new NotImplementedException();
    }

    public Genre? GetById(Guid? id)
    {
        throw new NotImplementedException();
    }

    public void Update(Genre update)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}