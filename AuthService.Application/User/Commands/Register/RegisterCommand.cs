﻿using MediatR;

namespace AuthService.Application.User.Commands.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}