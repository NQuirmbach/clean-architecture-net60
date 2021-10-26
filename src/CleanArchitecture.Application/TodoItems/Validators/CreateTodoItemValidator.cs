using CleanArchitecture.Application.TodoItems.Commands;

using FluentValidation;

namespace CleanArchitecture.Application.TodoItems.Validators;

public class CreateTodoItemValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemValidator()
    {
        RuleFor(m => m.Model)
            .NotNull()
            .ChildRules(m => {
                m.RuleFor(m => m.Text)
                    .NotEmpty()
                    .MaximumLength(100);
                m.RuleFor(m => m.ListId)
                    .NotEmpty();
            });
    }
}
