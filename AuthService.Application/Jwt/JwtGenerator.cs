using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Application.Jwt;

public class JwtGenerator
{
    private readonly AuthOptions _authOptions;

    public JwtGenerator(AuthOptions authOptions)
    {
        _authOptions = authOptions;
    }

    public string GenerateJWT(IdentityUser user, List<Claim> claims)
    {
        if (claims.Any(x => x.Type == ClaimTypes.NameIdentifier))
        {
            claims.RemoveAll(x => x.Type == ClaimTypes.NameIdentifier);
        }

        var Claims = GenerateBaseClaims(user);

        foreach (var claim in claims)
        {
            Claims.Add(claim);
        }

        return GenerateToken(Claims);
    }
    
    public string GenerateJWT(IdentityUser user)
    {
        var Claims = GenerateBaseClaims(user);

        return GenerateToken(Claims);
    }

    private List<Claim> GenerateBaseClaims(IdentityUser user)
    {
        var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        return Claims;
    }

    private string GenerateToken(List<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            audience: _authOptions.Audience,
            issuer: _authOptions.Issuer,
            expires: DateTime.UtcNow.AddDays(_authOptions.ExpiresTime),
            claims: claims,
            signingCredentials: GenerateSigningCredentials()
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private SigningCredentials GenerateSigningCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Secret));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}