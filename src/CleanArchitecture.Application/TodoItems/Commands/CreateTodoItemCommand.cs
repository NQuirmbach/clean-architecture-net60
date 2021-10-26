using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.Commands;

public class CreateTodoItemCommand : IRequest<TodoItemDto>
{
    public CreateTodoItemCommand(TodoItemAdd model)
    {
        Model = model;
    }

    public TodoItemAdd Model { get; }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
    {
        private readonly IAppContext _context;
        private readonly ILogger _logger;

        public CreateTodoListCommandHandler(IAppContext context, ILogger<CreateTodoItemCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var list = await _context.TodoLists.FindAsync(request.Model.ListId, cancellationToken);

            if (list == null) throw new EntityNotFoundException(nameof(TodoList), request.Model.ListId);

            var entity = request.Model.AdaptToTodoItem();

            await _context.TodoItems.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Todo list '{Text}' created in list '{List}'", entity.Text, list.Name);

            return entity.AdaptToDto();
        }
    }
}
