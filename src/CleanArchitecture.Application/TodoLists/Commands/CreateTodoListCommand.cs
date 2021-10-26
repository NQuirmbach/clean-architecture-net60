using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoLists.Commands;

public class CreateTodoListCommand : IRequest<TodoListDto>
{
    public CreateTodoListCommand(Guid createdBy, TodoListAdd model)
    {
        Model = model;
    }

    public TodoListAdd Model { get; }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, TodoListDto>
    {
        private readonly IAppContext _context;
        private readonly ILogger _logger;

        public CreateTodoListCommandHandler(IAppContext context, ILogger<CreateTodoListCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoListDto> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList(request.Model.Name);

            await _context.TodoLists.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Todo list '{Name}' created", entity.Name);

            return entity.AdaptToDto();
        }
    }
}
