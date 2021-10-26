using CleanArchitecture.Api.Infrastructure.Routing;
using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Application.TodoItems.Queries;
using CleanArchitecture.Domain.Models;

using MediatR;

namespace CleanArchitecture.Api.Routes;

public class TodoItemRoutes : IEndpointMapper
{
    public void Map(WebApplication app)
    {
        app.MapGet("/todolist/{listId}/item/{id}", async (IMediator mediator, Guid listId, Guid id) =>
        {
            var result = await mediator.Send(new GetTodoItemQuery(listId, id));

            return Results.Ok(result);
        })
        .WithName("Get item in List")
        .Produces<TodoItemDto>();

        app.MapPost("/todolist/{listId}/item", async (IMediator mediator, Guid listId, TodoItemAdd model) =>
        {
            var result = await mediator.Send(new CreateTodoItemCommand(listId, model));

            return Results.Created($"/api/todolist/{listId}/item/{result.Id}", result);
        })
        .WithName("Create item in List")
        .Produces<TodoItemDto>();

        app.MapPut("/todolist/{listId}/item/{id}", async (IMediator mediator, Guid listId, Guid id, TodoItemUpdate model) =>
        {
            var result = await mediator.Send(new UpdateTodoItemCommand(id, listId, model));

            return Results.Ok(result);
        })
        .WithName("Update item in List")
        .Produces<TodoItemDto>();


        app.MapDelete("/todolist/{listId}/item/{id}", async (IMediator mediator, Guid listId, Guid id) =>
        {
            await mediator.Send(new DeleteTodoItemCommand(listId, id));

            return Results.Accepted();
        })
        .WithName("Delete item in List");
    }
}

