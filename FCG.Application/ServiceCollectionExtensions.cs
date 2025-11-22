using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FCG.Application.Behaviors;
using FCG.Application.Validators;
using MediatR;

namespace FCG.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registrar todos os validadores
        services.AddValidatorsFromAssemblyContaining<CriarUsuarioDTOValidator>();
        
        // Registrar pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
