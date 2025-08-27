using System;
using System.Linq.Expressions;

namespace Domain.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
    }
}
