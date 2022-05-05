using WebAPI.Options;

namespace WebAPI.Extensions;

public static class ConfigurationExtension
{
    public static AuthConfig GetAuthConfiguration(this IConfiguration configuration)
    {
        var authConfig = configuration.GetSection("Auth");

        return new AuthConfig
        {
            Issuer = authConfig["Issuer"]!,
            Audience = authConfig["Audience"]!,
            Key = authConfig["Key"]!,
        };
    }
    
    public static List<string> GetAllRolePermissions(this IConfiguration configuration)
    {
        var permissions = configuration.GetSection("RolePermissions").Get<List<PermissionConfig>>();

        var list = permissions.SelectMany(x => x.Permissions).Select(x => x).Distinct().ToList();

        return list;
    }
}