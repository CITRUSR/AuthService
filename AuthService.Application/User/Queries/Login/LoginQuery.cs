using MediatR;

namespace AuthService.Application.User.Queries.Login;

public class LoginQuery : IRequest<AuthResponse>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}