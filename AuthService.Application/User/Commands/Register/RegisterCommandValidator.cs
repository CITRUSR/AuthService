﻿using FluentValidation;

namespace AuthService.Application.User.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
    }
}