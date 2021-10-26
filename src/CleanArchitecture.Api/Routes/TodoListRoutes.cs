using CleanArchitecture.Api.Infrastructure.Routing;
using CleanArchitecture.Application.TodoLists.Commands;
using CleanArchitecture.Application.TodoLists.Queries;
using CleanArchitecture.Domain.Models;

using MediatR;

namespace CleanArchitecture.Api.Routes;

public class TodoListRoutes : IEndpointMapper
{
    public void Map(WebApplication app)
    {
        app.MapGet("/todolist/{personId}", async (IMediator mediator, Guid personId) =>
        {
            var result = await mediator.Send(new GetPersonTodoListsQuery(personId));

            return Results.Ok(result);
        })
        .WithName("GetTodoLists");

        app.MapPost("/todolist/{personId}", async (IMediator mediator, Guid personId, TodoListAdd model) =>
        {
            var result = await mediator.Send(new CreateTodoListCommand(personId, model));

            return Results.Ok(result);
        });
    }
}

