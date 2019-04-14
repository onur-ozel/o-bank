using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Infrastructure.Utils
{
    public static class APIUtils
    {
        public static string CapitalizeFirstLetter(this string value)
        {
            return value[0].ToString().ToUpperInvariant() + value.Substring(1, value.Length - 1);
        }

        public static IQueryable<TEntity> ParseFields<TEntity>(this IQueryable<TEntity> query, String fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return query;
            }

            string[] searchArray = fields.Split(",");

            Expression queryExpr = query.Expression;
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

            queryExpr = Expression.Call(
                typeof(Queryable),
                "Select",
                new Type[] { query.ElementType },
                queryExpr, Expression.Quote(Expression.Lambda(predicateBody, parameter))
            );

            return query.Provider.CreateQuery<TEntity>(queryExpr);
        }

        public static IQueryable<TEntity> ParseSearches<TEntity>(this IQueryable<TEntity> query, String searches)
        {
            if (string.IsNullOrWhiteSpace(searches))
            {
                return query;
            }

            string[] searchArray = searches.Split(",");

            Expression queryExpr = query.Expression;
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");
            Expression predicateBody = null;

            for (int i = 0; i < searchArray.Length; i++)
            {
                string searchValue = string.Empty;
                string searchField = string.Empty;
                string searchExpression = string.Empty;

                searchField = Regex.Split(searchArray[i], "\\[.*]")[0].CapitalizeFirstLetter();
                searchValue = Regex.Split(searchArray[i], "\\[.*]")[1];
                searchExpression = Regex.Replace(searchArray[i].Substring(searchField.Length, searchArray[i].IndexOf(searchValue) - searchField.Length), "[\\[\\]]", "");


                PropertyInfo property = query.ElementType.GetProperty(searchField);


                Expression left = Expression.Property(parameter, property);

                TypeConverter typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
                object searchValueTyped = typeConverter.ConvertFromString(searchValue);

                Expression right = Expression.Constant(searchValueTyped, property.PropertyType);
                Expression expression = Expression.Equal(left, right);

                if (predicateBody == null)
                {
                    predicateBody = expression;
                }
                else
                {
                    predicateBody = Expression.And(predicateBody, expression);
                }
            }

            queryExpr = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { query.ElementType },
                queryExpr, Expression.Quote(Expression.Lambda(predicateBody, parameter))
            );

            return query.Provider.CreateQuery<TEntity>(queryExpr);
        }
        public static IQueryable<TEntity> ParseSort<TEntity>(this IQueryable<TEntity> query, String sorts)
        {
            if (string.IsNullOrWhiteSpace(sorts))
            {
                return query;
            }

            string[] sortArray = sorts.Split(",");

            Expression queryExpr = query.Expression;

            for (int i = 0; i < sortArray.Length; i++)
            {
                string sortDirection = string.Empty;
                string sortField = string.Empty;

                if (sortArray[i][0] == '+')
                {
                    sortDirection = i == 0 ? "OrderBy" : "ThenBy";

                }
                else if (sortArray[i][0] == '-')
                {
                    sortDirection = i == 0 ? "OrderByDescending" : "ThenByDescending";
                }
                else
                {
                    //TODO throw undefined order error;
                }

                //TODO check validation
                sortField = sortArray[i].Substring(1, sortArray[i].Length - 1).CapitalizeFirstLetter();

                ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

                PropertyInfo property = query.ElementType.GetProperty(sortField);
                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);

                queryExpr = Expression.Call(
                    typeof(Queryable),
                    sortDirection,
                    new Type[] { query.ElementType, property.PropertyType },
                    queryExpr, Expression.Quote(Expression.Lambda(propertyAccess, parameter))
                );
            }

            return query.Provider.CreateQuery<TEntity>(queryExpr);
        }
    }
}
