﻿using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.Commands;

public class DeleteTodoItemCommand : IRequest
{
    public DeleteTodoItemCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IAppContext _context;
        private readonly ILogger _logger;

        public DeleteTodoItemCommandHandler(IAppContext context, ILogger<DeleteTodoItemCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if (entity == null) throw new EntityNotFoundException(nameof(TodoItem), request.Id);

            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Item '{ItemId}' has been delete from list '{ListId}'", request.Id, entity.ListId);

            return Unit.Value;
        }
    }
}
