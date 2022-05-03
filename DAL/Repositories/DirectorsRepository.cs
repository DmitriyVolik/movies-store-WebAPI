using DAL.DB;
using DAL.Entities;
using DAL.Repositories.Abstractions;

namespace DAL.Repositories;

public class DirectorsRepository : IRepository<Director, Director>
{
    private readonly Context _context;

    public DirectorsRepository(Context context)
    {
        _context = context;
    }

    public void Add(Director director)
    {
        _context.Add(director);
    }

    public IEnumerable<Director> Get()
    {
        throw new NotImplementedException();
    }

    public Director? GetById(Guid? id)
    {
        throw new NotImplementedException();
    }

    public void Update(Director movieUpdate)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}