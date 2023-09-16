using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Application.Jwt;

public class JwtGenerator
{
    private readonly AuthOptions _authOptions;

    public JwtGenerator(AuthOptions authOptions)
    {
        _authOptions = authOptions;
    }

    public string GenerateJWT(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Secret));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            audience: _authOptions.Audience,
            issuer: _authOptions.Issuer,
            expires: DateTime.UtcNow.AddDays(_authOptions.ExpiresTime),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}