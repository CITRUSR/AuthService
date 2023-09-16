using System.Security.Claims;
using AuthService.Application.Jwt;
using AuthService.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.User.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand,RegisterResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtGenerator _jwtGenerator;

    public RegisterCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        AppUser user = new AppUser
        {
            UserName = request.UserName,
        };

        RegisterResponse response = new RegisterResponse();

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            response.Errors = result.Errors;
            return response;
        }

        await _signInManager.SignInAsync(user, false);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
        };

        response.Jwt = _jwtGenerator.GenerateJWT(claims);

        return response;
    }
}