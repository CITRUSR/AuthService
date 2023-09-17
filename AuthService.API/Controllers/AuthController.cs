using AuthService.API.Models;
using AuthService.Application.User.Commands.Register;
using AuthService.Application.User.Queries.Login;
using FluentValidation.Results;
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
    /// <summary>
    /// Registration user
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Jwt token</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Validation error</response>
    [HttpPost]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<ValidationFailure>),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody]RegisterDTO model)
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
    
    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Jwt token</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Validation error</response>
    [HttpPost]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<ValidationFailure>),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody]LoginDTO model)
    {
        var query = new LoginQuery
        {
            UserName = model.UserName,
            Password = model.Password,
        };

        var jwt = await _mediator.Send(query);

        return Ok(jwt);
    }
}