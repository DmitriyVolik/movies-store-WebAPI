namespace DAL.Repositories.Abstractions;

public interface IRepository<out T, in K>
{
    public void Add(K obj);
    
    public IEnumerable<T> Get();
    
    public T? GetById(Guid? id);

    public void Update(K update);
    
    public void Delete(Guid id);
}