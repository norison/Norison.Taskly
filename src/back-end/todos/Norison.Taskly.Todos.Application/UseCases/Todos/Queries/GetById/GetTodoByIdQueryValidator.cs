using FluentValidation;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetById;

public class GetTodoByIdQueryValidator : AbstractValidator<GetTodoByIdQuery>
{
    public GetTodoByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}