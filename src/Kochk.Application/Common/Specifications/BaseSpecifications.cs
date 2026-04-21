using System.Linq.Expressions;
using Kochk.Application.Common.Interfaces.Specifications;
using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Specifications;

public class BaseSpecifications<T> : ISpecifications<T>
    where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; } = default!;
    public List<Expression<Func<T, object>>> Includes { get; set; } = [];
    public Expression<Func<T, object>> OrderBy { get; set; } = default!;
    public Expression<Func<T, object>> OrderByDec { get; set; } = default!;
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool IsPaginationEnabled { get; set; }

    // private Func<T, bool> _criteria = default!;

    public BaseSpecifications() { }

    public BaseSpecifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    // public void AddCriteria(Func<T, bool> criteria)
    // {
    //     _criteria += criteria;
    // }
    //
    // public void SetCriteria()
    // {
    // Criteria=_criteria;
    // }

    public void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    public void AddOrderByDec(Expression<Func<T, object>> orderByDec)
    {
        OrderByDec = orderByDec;
    }

    public void ApplyPagination(int skip, int take)
    {
        IsPaginationEnabled = true;
        Skip = skip;
        Take = take;
    }
}
