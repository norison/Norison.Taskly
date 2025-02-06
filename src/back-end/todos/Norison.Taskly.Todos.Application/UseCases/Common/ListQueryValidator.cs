using FluentValidation;

namespace Norison.Taskly.Todos.Application.UseCases.Common;

public class ListQueryValidator<T> : AbstractValidator<ListQuery<T>>
{
    public ListQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}