using AuthService.API.Models;
using AuthService.Application.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        var command = new RegisterCommand
        {
            UserName = model.UserName,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword,
        };

        var jwt = await _mediator.Send(command);
        return Ok(jwt);
    }
}