using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoItems.Validators
{
    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        const int MAX_ITEMS_INPROGRESS = 3;

        public TodoItemValidator(IAppContext context)
        {
            RuleFor(m => m).MustAsync(async (item, cancellationToken) =>
            {
                if (item.Status != Domain.Enums.ItemStatus.InProgress)
                    return true;

                // check current item count in list that have status 'In progress'
                var inProgressCount = await context.TodoItems
                    .Where(m => m.ListId == item.ListId && m.Id != item.Id && m.Status == Domain.Enums.ItemStatus.InProgress)
                    .CountAsync(cancellationToken);

                return inProgressCount < MAX_ITEMS_INPROGRESS;
            }).WithMessage($"Only {MAX_ITEMS_INPROGRESS} items in one list are allowed to be in status 'InProgress'");
        }
    }
}
