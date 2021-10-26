using System.Reflection;

using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.TodoItems.Validators;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<TodoItemValidator>();
    }
}
