using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CsvParser.Extensions
{
    public static class OrderByExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string propertyName)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            Expression propertyExpression = Expression.Property(parameterExpression, propertyName);
            var resultExpression = Expression.Lambda(propertyExpression, parameterExpression);

            var lambda = resultExpression.Compile();

            Type enumerableType = typeof(Enumerable);
            var methods = enumerableType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            var selectedMethods = methods.Where(m => m.Name == "OrderBy" && m.GetParameters().Count() == 2);

            var method = selectedMethods.First();

            method = method.MakeGenericMethod(typeof(T), propertyExpression.Type);

            var result = (IEnumerable<T>)method.Invoke(null, new object[] { source, lambda });

            return result;
        }
    }
}
