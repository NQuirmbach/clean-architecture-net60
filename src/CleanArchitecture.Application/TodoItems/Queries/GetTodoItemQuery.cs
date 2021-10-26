using System;
using System.Collections.Generic;
using System.Text;

using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Queries;

public class GetTodoItemQuery : IRequest<TodoItemDto>
{
    public GetTodoItemQuery(Guid listId, Guid id)
    {
        ListId = listId;
        Id = id;
    }

    public Guid ListId { get; }
    public Guid Id { get; }
}


public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemDto>
{
    private readonly IAppContext _context;

    public GetTodoItemQueryHandler(IAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TodoItemDto> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .SingleOrDefaultAsync(m => m.Id == request.Id && m.ListId == request.ListId, cancellationToken);

        if (entity == null) throw new EntityNotFoundException(nameof(TodoItem), request.Id);

        return entity.AdaptToDto();
    }
}
