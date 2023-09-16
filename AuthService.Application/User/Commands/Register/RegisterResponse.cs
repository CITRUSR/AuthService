using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.User.Commands.Register;

public class RegisterResponse
{
    public string Jwt { get; set; } = string.Empty;
    public IEnumerable<IdentityError>? Errors { get; set; }
}