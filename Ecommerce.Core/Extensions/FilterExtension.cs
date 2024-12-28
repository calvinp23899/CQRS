using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Extensions
{
    public static class FilterExtension
    {
        public static Expression<Func<T, bool>> AddFilter<T>(
            this Expression<Func<T, bool>>? existingFilter, 
            Expression<Func<T, bool>> newFilter,
            bool isUseOr = false)
        {
            if (existingFilter == null)
            {
                return newFilter;
            }

            if (newFilter == null)
            {
                return existingFilter;
            }

            // Combine the two filters using Expression.AndAlso
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftVisitor = new ReplaceExpressionVisitor(existingFilter.Parameters[0], parameter);
            var left = leftVisitor.Visit(existingFilter.Body);

            var rightVisitor = new ReplaceExpressionVisitor(newFilter.Parameters[0], parameter);
            var right = rightVisitor.Visit(newFilter.Body);

            var combined = isUseOr
                ? Expression.OrElse(left, right)
                : Expression.AndAlso(left, right);

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        // Helper class to replace expression parameters
        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldValue ? _newValue : node;
            }
        }
    }
}
