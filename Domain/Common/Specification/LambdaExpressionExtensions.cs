using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Domain.Common.Specification
{
    public static class LambdaExpressionExtensions
    {
        public static Expression<Func<T1, bool>> CombineExpression<T1, T2>(this Expression<Func<T1, bool>> first, Expression<Func<T2, bool>> second, string nameProperty, out ParameterExpression paramBase)
        {
            //Se crea el parametro base con la propiedad de navegacion
            var paramT2 = Expression.Parameter(typeof(T2), $"{first.Parameters.First()}.{nameProperty}");
            //se crea la propiedad base 
            paramBase = Expression.Parameter(typeof(T1), first.Parameters.First().Name);
            //se reemplazan los parametros por el base
            var exprNavigation = ParameterReplacer.Replace(paramT2, second);
            //se crea el nuevo parametro {x} para la  Expression<Func<T1, bool>>
            var filterT2 = Expression.Lambda<Func<T1, bool>>(exprNavigation.Body, paramBase).ToString();
            //Se crea la nueva Expression tipando los {x} al objeto base
            return DynamicExpressionParser.ParseLambda<T1, bool>(ParsingConfig.Default, false, filterT2, paramBase);
        }

        public static Expression<Func<T1, bool>> CombineAndExpression<T1, T2>(this Expression<Func<T1, bool>> first, Expression<Func<T2, bool>> second, string nameProperty)
        {
            var exprNewAnd = first.CombineExpression(second, nameProperty, out var paramBase);
            var exprAndAlso = Expression.AndAlso(ParameterReplacer.Replace(paramBase, first).Body, ParameterReplacer.Replace(paramBase, exprNewAnd).Body);
            return Expression.Lambda<Func<T1, bool>>(exprAndAlso, paramBase);
        }

        public static Expression<Func<T1, bool>> CombineOrExpression<T1, T2>(this Expression<Func<T1, bool>> first, Expression<Func<T2, bool>> second, string nameProperty)
        {
            var exprNewOr = first.CombineExpression(second, nameProperty, out var paramBase);
            var exprOr = Expression.Or(ParameterReplacer.Replace(paramBase, first).Body, ParameterReplacer.Replace(paramBase, exprNewOr).Body);
            return Expression.Lambda<Func<T1, bool>>(exprOr, paramBase);
        }
    }
}
