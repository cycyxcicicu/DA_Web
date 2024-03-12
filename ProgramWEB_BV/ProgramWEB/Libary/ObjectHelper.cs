using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace ProgramWEB.Libary
{
    public class ObjectHelper
    {
        public static IEnumerable<T> OrderByDynamic<T>(IEnumerable<T> items, string sortby, string sort_direction)
        {
            var property = typeof(T).GetProperty(sortby);

            var result = typeof(ObjectHelper)
                .GetMethod("OrderByDynamic_Private", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(typeof(T), property.PropertyType)
                .Invoke(null, new object[] { items, sortby, sort_direction });

            return (IEnumerable<T>)result;
        }

        private static IEnumerable<T> OrderByDynamic_Private<T, TKey>(IEnumerable<T> items, string sortby, string sort_direction)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            Expression<Func<T, TKey>> property_access_expression =
                Expression.Lambda<Func<T, TKey>>(
                    Expression.Property(parameter, sortby),
                    parameter);

            if (sort_direction == "asc")
            {
                return items.OrderBy(property_access_expression.Compile());
            }

            if (sort_direction == "desc")
            {
                return items.OrderByDescending(property_access_expression.Compile());
            }

            throw new Exception("Invalid Sort Direction");
        }
    }
}