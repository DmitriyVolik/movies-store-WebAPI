using DAL.Entities;

namespace BLL.Services.Abstractions;

public interface IDirectorsService
{
    public void AddDirector(Director director);
}