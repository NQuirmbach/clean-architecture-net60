using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Models;

using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.Commands;

public class UpdateTodoItemCommand : IRequest<TodoItemDto>
{
    public UpdateTodoItemCommand(Guid id, Guid listId, TodoItemUpdate model)
    {
        Id = id;
        ListId = listId;
        Model = model;
    }

    public Guid Id { get; }
    public Guid ListId { get; }
    public TodoItemUpdate Model { get; }


    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItemDto>
    {
        private readonly IAppContext _context;
        private readonly IValidator<TodoItem> _validator;
        private readonly ILogger _logger;

        const int MAX_INPROGRESS_COUNT = 3;

        public UpdateTodoItemCommandHandler(IAppContext context, IValidator<TodoItem> validator, ILogger<UpdateTodoItemCommand> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TodoItemDto> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems
                .SingleOrDefaultAsync(m => m.Id == request.Id && m.ListId == request.ListId, cancellationToken);

            if (entity == null) throw new EntityNotFoundException(nameof(TodoItem), request.Id);

            request.Model.AdaptTo(entity);

            await _validator.ValidateAndThrowAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.AdaptToDto();
        }
    }
}
