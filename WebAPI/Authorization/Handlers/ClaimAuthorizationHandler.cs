using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Authorization.Requirements;
using WebAPI.Options;

namespace WebAPI.Authorization.Handlers;

public class ClaimAuthorizationHandler : AuthorizationHandler<ClaimAuthorizationRequirement>
{
    private IConfiguration _configuration;
    
    public ClaimAuthorizationHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, ClaimAuthorizationRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var role = context.User.FindFirst(ClaimTypes.Role)!.Value;
            
            var permissions = 
                _configuration.GetSection("RolePermissions").Get<List<PermissionOptions>>()
                    .First(x=>x.Role == role);

            if (permissions.Permissions.Contains(requirement.Permission)) context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}