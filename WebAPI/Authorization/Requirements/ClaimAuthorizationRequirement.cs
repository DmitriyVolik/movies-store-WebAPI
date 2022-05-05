using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Extensions;
using WebAPI.Options;

namespace WebAPI.Authorization.Requirements;

public class ClaimAuthorizationRequirement : IAuthorizationRequirement
{
    public ClaimAuthorizationRequirement(string permission)
    {
        Permission = permission;
    }
    
    public string Permission { get; private set; }
}