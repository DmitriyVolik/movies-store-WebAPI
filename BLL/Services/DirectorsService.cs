using BLL.Services.Abstractions;
using DAL.Entities;
using DAL.Repositories.Abstractions;

namespace BLL.Services;

internal class DirectorsService : IDirectorsService
{
    private readonly IUnitOfWork _unitOfWork;

    public DirectorsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddDirector(Director director)
    {
        _unitOfWork.Directors.Add(director);
        _unitOfWork.Save();
    }
}