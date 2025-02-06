using Microsoft.Extensions.Options;

using Norison.Taskly.Todos.Domain.AggregateRoots;

using Sieve.Models;
using Sieve.Services;

namespace Norison.Taskly.Todos.Persistence;

public class ApplicationSieveProcessor : SieveProcessor
{
    public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
    {
    }

    public ApplicationSieveProcessor(
        IOptions<SieveOptions> options,
        ISieveCustomSortMethods customSortMethods) : base(options, customSortMethods)
    {
    }

    public ApplicationSieveProcessor(
        IOptions<SieveOptions> options,
        ISieveCustomFilterMethods customFilterMethods) : base(options, customFilterMethods)
    {
    }

    public ApplicationSieveProcessor(
        IOptions<SieveOptions> options,
        ISieveCustomSortMethods customSortMethods,
        ISieveCustomFilterMethods customFilterMethods) : base(options, customSortMethods, customFilterMethods)
    {
    }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.Property<Todo>(x => x.Title).CanFilter().CanSort();
        mapper.Property<Todo>(x => x.Description).CanFilter();
        mapper.Property<Todo>(x => x.Status).CanFilter().CanSort();
        mapper.Property<Todo>(x => x.CreatedDateTime).CanFilter().CanSort();
        mapper.Property<Todo>(x => x.LastEditedDatetime).CanFilter().CanSort();
        return mapper;
    }
}