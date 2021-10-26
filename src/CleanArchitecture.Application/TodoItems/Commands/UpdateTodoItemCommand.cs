using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.Commands;

public class UpdateTodoItemCommand : IRequest<TodoItemDto>
{
    public UpdateTodoItemCommand(Guid id, TodoItemUpdate model)
    {
        Id = id;
        Model = model;
    }

    public Guid Id { get; }
    public TodoItemUpdate Model { get; }


    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItemDto>
    {
        private readonly IAppContext _context;
        private readonly ILogger _logger;

        const int MAX_INPROGRESS_COUNT = 3;

        public UpdateTodoItemCommandHandler(IAppContext context, ILogger<UpdateTodoItemCommand> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TodoItemDto> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if (entity == null) throw new EntityNotFoundException(nameof(TodoItem), request.Id);

            await CheckStatusPolicy(entity, cancellationToken);

            request.Model.AdaptTo(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.AdaptToDto();
        }

        /// <summary>
        /// Check for all items inside the current list. Only 3 items are allowed with status 'InProgress'
        /// </summary>
        /// <param name="item">Item</param>
        private async Task CheckStatusPolicy(TodoItem item, CancellationToken cancellationToken)
        {
            if (item.Status != Domain.Enums.ItemStatus.InProgress)
                return;

            // check current item count in list that have status 'In progress'
            var inProgressCount = await _context.TodoItems
                .Where(m => m.ListId == item.ListId && m.Id != item.Id && m.Status == Domain.Enums.ItemStatus.InProgress)
                .CountAsync(cancellationToken);

            if (inProgressCount >= MAX_INPROGRESS_COUNT)
            {
                throw new MaxItemInProgressException(MAX_INPROGRESS_COUNT);
            }
        }
    }
}
