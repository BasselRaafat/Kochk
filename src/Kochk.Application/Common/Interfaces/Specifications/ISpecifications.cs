using System.Linq.Expressions;

namespace Kochk.Application.Common.Interfaces.Specifications;

public interface ISpecifications<T>
    where T : class
{
    Expression<Func<T, bool>> Criteria { set; get; }
    List<Expression<Func<T, object>>> Includes { set; get; }
    Expression<Func<T, object>> OrderBy { set; get; }
    Expression<Func<T, object>> OrderByDec { set; get; }
    int Skip { set; get; }
    int Take { set; get; }
    bool IsPaginationEnabled { set; get; }
    void AddOrderBy(Expression<Func<T, object>> orderBy);
    void AddOrderByDec(Expression<Func<T, object>> orderByDec);
    void ApplyPagination(int pageSize, int pageIndex);
}
