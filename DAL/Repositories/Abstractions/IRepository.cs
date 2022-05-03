namespace DAL.Repositories.Abstractions;

public interface IRepository<out T, in TK>
{
    public void Add(TK obj);
    
    public IEnumerable<T> Get();
    
    public T? GetById(Guid? id);

    public void Update(TK update);
    
    public void Delete(Guid id);
}