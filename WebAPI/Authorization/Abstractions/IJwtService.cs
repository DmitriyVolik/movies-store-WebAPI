using Models.Models;
using WebAPI.Options;

namespace WebAPI.Authorization.Abstractions;

public interface IJwtService
{
    public object GetJwtResponse(UserModel userModel, AuthOptions authOptions);
}