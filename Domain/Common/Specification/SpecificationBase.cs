using System;
using System.Linq.Expressions;

namespace Domain.Specification
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {

        public virtual Expression<Func<T, bool>> SpecExpression { get; }
    }
}
