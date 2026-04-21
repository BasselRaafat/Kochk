using System.Linq.Expressions;
using Kochk.Application.Common.Interfaces.Specifications;
using Kochk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kochk.Infrastructure.Evaluator;

public static class SpecificationEvaluator<T>
    where T : class
{
    public static IQueryable<T> CreateQuery(IQueryable<T> set, ISpecifications<T> specs)
    {
        var query = set;
        if (specs.Criteria is not null)
            query = query.Where(specs.Criteria);
        if (specs.OrderBy is not null)
            query = query.OrderBy(specs.OrderBy);
        else if (specs.OrderByDec is not null)
            query = query.OrderByDescending(specs.OrderByDec);
        if (specs.IsPaginationEnabled)
            query = query.Skip(specs.Skip).Take(specs.Take);

        return specs.Includes.Aggregate(query, (newSet, include) => newSet.Include(include));
    }

    public static IQueryable<TResult> Project<TResult>(
        IQueryable<T> query,
        Expression<Func<T, TResult>> selector
    )
    {
        return query.Select(selector);
    }
}
