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
        if (_context.Directors.FirstOrDefault(x=>x.FullName==director.FullName) is null)
        {
            _context.Add(director);
        }
        else
        {
            throw new Exception("Director with this name already exists");
        }
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