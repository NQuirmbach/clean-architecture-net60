using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;

using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.Commands;

public class CreateTodoItemCommand : IRequest<TodoItemDto>
{
    public CreateTodoItemCommand(Guid listId, TodoItemAdd model)
    {
        ListId = listId;
        Model = model;
    }

    public Guid ListId { get; }
    public TodoItemAdd Model { get; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
{
    private readonly IAppContext _context;
    private readonly IValidator<TodoItem> _validator;
    private readonly ILogger _logger;

    public CreateTodoItemCommandHandler(IAppContext context, IValidator<TodoItem> validator, ILogger<CreateTodoItemCommand> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var list = await _context.TodoLists.SingleOrDefaultAsync(m => m.Id == request.ListId, cancellationToken);

        if (list == null) throw new EntityNotFoundException(nameof(TodoList), request.ListId);

        var entity = request.Model.AdaptToTodoItem();
        entity.ListId = request.ListId;

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        await _context.TodoItems.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Todo list '{Text}' created in list '{List}'", entity.Text, list.Name);

        return entity.AdaptToDto();
    }
}
