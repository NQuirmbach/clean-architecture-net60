using CleanArchitecture.Application.TodoLists.Commands;

using FluentValidation;

namespace CleanArchitecture.Application.TodoLists.Validators;

public class CreateTodoListValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListValidator()
    {
        RuleFor(m => m.Model)
            .NotNull()
            .ChildRules(m =>            
                m.RuleFor(m => m.Name)
                    .NotEmpty()
                    .MaximumLength(100)
            );
    }
}
