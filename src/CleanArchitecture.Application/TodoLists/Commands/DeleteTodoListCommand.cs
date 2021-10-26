using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoLists.Commands;

public class DeleteTodoListCommand : IRequest
{
    public DeleteTodoListCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly IAppContext _context;
        private readonly ILogger _logger;

        public DeleteTodoListCommandHandler(IAppContext context, ILogger<DeleteTodoListCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists.FindAsync(request.Id);

            if (entity == null) throw new EntityNotFoundException(nameof(TodoList), request.Id);

            _context.TodoLists.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
