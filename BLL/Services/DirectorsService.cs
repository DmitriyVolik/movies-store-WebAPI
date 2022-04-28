using DAL.Entities;
using DAL.Repositories.Abstractions;

namespace BLL.Services;

public class DirectorsService
{
    private readonly IUnitOfWork _unitOfWork;

    public DirectorsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(Director director)
    {
        _unitOfWork.Directors.Add(director);
        _unitOfWork.Save();
    }
}