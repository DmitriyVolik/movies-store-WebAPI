using DAL.DB;

namespace DAL.Repositories.Abstractions;

public interface IRepository<out T, in TDto>
{
    public void Add(TDto movie);
    
    public IEnumerable<T> Get();
    
    public T? GetById(Guid id);

    public void Update(TDto movieUpdate);
    
    public void Delete(Guid id);
}