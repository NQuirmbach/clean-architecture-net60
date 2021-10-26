using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoLists.Queries;

public class GetTodoListsQuery : IRequest<IEnumerable<TodoListDto>>
{
    public GetTodoListsQuery()
    { }



    public class GetTodoListsQueryHandler : IRequestHandler<GetTodoListsQuery, IEnumerable<TodoListDto>>
    {
        private readonly IAppContext _context;

        public GetTodoListsQueryHandler(IAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoListDto>> Handle(GetTodoListsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.TodoLists
                .Include(m => m.Items)
                .ToListAsync(cancellationToken);

            return entities.Select(e => e.AdaptToDto());
        }
    }
}
