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
        app.MapGet("/todolist", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetTodoListsQuery());

            return Results.Ok(result);
        })
        .WithName("GetTodoLists")
        .Produces<IEnumerable<TodoListDto>>();

        app.MapPost("/todolist", async (IMediator mediator, TodoListAdd model) =>
        {
            var result = await mediator.Send(new CreateTodoListCommand(model));

            return Results.Ok(result);
        })
        .WithName("Post TodoList")
        .Produces<TodoListDto>();


        app.MapDelete("/todolist/{id}", async (IMediator mediator, Guid id) =>
        {
            await mediator.Send(new DeleteTodoListCommand(id));

            return Results.Accepted();
        })
        .WithName("Delete TodoList");
    }
}

