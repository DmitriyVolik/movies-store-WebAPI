using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Options;

public class AuthConfig
{
    public string Issuer { get; set; }

    public string Audience { get; set; }
    
    public string Key { get; set; }
}