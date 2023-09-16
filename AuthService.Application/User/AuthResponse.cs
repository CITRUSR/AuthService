using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.User;

public class AuthResponse
{
    public string Jwt { get; set; } = string.Empty;
    public IEnumerable<IdentityError>? Errors { get; set; }
}