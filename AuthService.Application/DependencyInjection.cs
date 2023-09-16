using System.Reflection;
using AuthService.Application.Common.Behaviour;
using AuthService.Application.Jwt;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));
        services.AddSingleton<AuthOptions>();
        services.AddSingleton<JwtGenerator>();

        return services;
    }
}