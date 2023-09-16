using System.Security.Claims;
using AuthService.Application.Jwt;
using AuthService.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.User.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly JwtGenerator _jwtGenerator;

    public LoginQueryHandler(SignInManager<AppUser> signInManager, JwtGenerator jwtGenerator, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
    }

    public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        AuthResponse response = new AuthResponse();

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (!result.Succeeded)
        {
            response.Errors = new List<IdentityError>
            {
                new IdentityError
                {
                    Description = "Invalid login attempt",
                }
            };
            return response;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        response.Jwt = _jwtGenerator.GenerateJWT(user);

        return response;
    }
}