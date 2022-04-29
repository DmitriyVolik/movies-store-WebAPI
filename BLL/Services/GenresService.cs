using DAL.Entities;
using DAL.Repositories.Abstractions;

namespace BLL.Services;

public class GenresService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenresService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(Genre genre)
    {
        throw new NotImplementedException();
    }
}