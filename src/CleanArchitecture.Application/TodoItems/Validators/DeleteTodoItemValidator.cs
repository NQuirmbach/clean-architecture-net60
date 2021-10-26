using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Application.TodoLists.Queries;

using FluentValidation;

namespace CleanArchitecture.Application.TodoItems.Validators;

public class DeleteTodoItemValidator : AbstractValidator<DeleteTodoItemCommand>
{
    public DeleteTodoItemValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty();
    }
}
