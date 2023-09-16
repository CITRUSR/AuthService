using System.Security.Permissions;
using Microsoft.Extensions.Configuration;

namespace AuthService.Application.Jwt;

public class AuthOptions
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Secret { get; set; }
    public int ExpiresTime { get; set; }
    private readonly IConfiguration _configuration;

    public AuthOptions(IConfiguration configuration)
    {
        _configuration = configuration;
        Audience = _configuration["Auth:Audience"];
        Issuer = _configuration["Auth:Issuer"];
        Secret = _configuration["Auth:Secret"];
        ExpiresTime = Convert.ToInt32(_configuration["Auth:ExpressTimeInDays"]);
    }
}