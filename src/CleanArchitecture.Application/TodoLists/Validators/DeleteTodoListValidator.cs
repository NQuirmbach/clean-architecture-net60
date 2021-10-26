using CleanArchitecture.Application.TodoLists.Commands;

using FluentValidation;

namespace CleanArchitecture.Application.TodoLists.Validators;

public class DeleteTodoListValidator : AbstractValidator<DeleteTodoListCommand>
{
    public DeleteTodoListValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty();
    }
}
