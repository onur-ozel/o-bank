using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Customer.API.Infrastructure.Utils {
    public class TestA {
        public string Foo { get; set; }
    }

    public class PropertyIgnoreSerializerContractResolver : DefaultContractResolver {
        private readonly Dictionary<Type, HashSet<string>> _ignores;
        public PropertyIgnoreSerializerContractResolver () {
            _ignores = new Dictionary<Type, HashSet<string>> ();
        }

        public void IgnoreProperty (Type type, params string[] jsonPropertyNames) {
            if (!_ignores.ContainsKey (type))
                _ignores[type] = new HashSet<string> ();

            foreach (var prop in jsonPropertyNames)
                _ignores[type].Add (prop);
        }

        protected override JsonProperty CreateProperty (MemberInfo member, MemberSerialization memberSerialization) {
            var property = base.CreateProperty (member, memberSerialization);

            if (IsIgnored (property.DeclaringType, property.PropertyName)) {
                property.ShouldSerialize = i => false;
                property.Ignored = true;
            }

            return property;
        }

        private bool IsIgnored (Type type, string jsonPropertyName) {
            if (!_ignores.ContainsKey (type))
                return false;

            return _ignores[type].Contains (jsonPropertyName);
        }
    }
    public static class APIUtils {
        public static string UpperFirstLetter (this string value) {
            return value[0].ToString ().ToUpperInvariant () + value.Substring (1, value.Length - 1);
        }

        public static string LowerFirstLetter (this string value) {
            return value[0].ToString ().ToLowerInvariant () + value.Substring (1, value.Length - 1);
        }

        private static MethodInfo GetMethodInfo<T1, T2> (Func<T1, T2> f, T1 unused1) {
            return f.Method;
        }

        private static MethodInfo GetMethodInfo<T1, T2, T3> (Func<T1, T2, T3> f, T1 unused1, T2 unused2) {
            return f.Method;
        }

        private static MethodInfo GetMethodInfo<T1, T2, T3, T4> (Func<T1, T2, T3, T4> f, T1 unused1, T2 unused2, T3 unused3) {
            return f.Method;
        }

        private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5> (Func<T1, T2, T3, T4, T5> f, T1 unused1, T2 unused2, T3 unused3, T4 unused4) {
            return f.Method;
        }

        private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6> (Func<T1, T2, T3, T4, T5, T6> f, T1 unused1, T2 unused2, T3 unused3, T4 unused4, T5 unused5) {
            return f.Method;
        }

        private static MethodInfo GetMethodInfo<T1, T2, T3, T4, T5, T6, T7> (Func<T1, T2, T3, T4, T5, T6, T7> f, T1 unused1, T2 unused2, T3 unused3, T4 unused4, T5 unused5, T6 unused6) {
            return f.Method;
        }

        public static IQueryable<TEntity> DynamicTake<TEntity> (this IQueryable<TEntity> query, int? limit) {
            if (limit == null) {
                return query;
            }

            Expression queryExpr = query.Expression;

            queryExpr = Expression.Call (
                null,
                GetMethodInfo (Queryable.Take, query, limit.Value),
                new Expression[] { query.Expression, Expression.Constant (limit.Value) }
            );

            return query.Provider.CreateQuery<TEntity> (queryExpr);
        }

        public static IQueryable<TEntity> DynamicSkip<TEntity> (this IQueryable<TEntity> query, int? offset) {
            if (offset == null) {
                return query;
            }

            Expression queryExpr = query.Expression;

            queryExpr = Expression.Call (
                null,
                GetMethodInfo (Queryable.Skip, query, offset.Value),
                new Expression[] { query.Expression, Expression.Constant (offset.Value) }
            );

            return query.Provider.CreateQuery<TEntity> (queryExpr);
        }

        public static IQueryable<dynamic> DynamicSelect<TEntity> (this IQueryable<TEntity> query, String fields) {
            if (string.IsNullOrWhiteSpace (fields)) {
                return (IQueryable<dynamic>) query;
            }

            string[] fieldArray = fields.Split (",");

            Expression queryExpr = query.Expression;

            Func<TEntity, dynamic> selectorFunc = (element) => {
                dynamic filteredObject = new System.Dynamic.ExpandoObject ();
                for (int i = 0; i < fieldArray.Length; i++) {
                    ((IDictionary<String, Object>) filteredObject) [fieldArray[i].LowerFirstLetter ()] = query.ElementType.GetProperty (fieldArray[i].UpperFirstLetter ()).GetValue (element, null);
                }

                return filteredObject;
            };

            Expression<Func<TEntity, dynamic>> selector = x => selectorFunc (x);

            queryExpr = Expression.Call (
                null,
                GetMethodInfo (Queryable.Select, query, selector),
                new Expression[] { query.Expression, Expression.Quote (selector) }
            );

            return query.Provider.CreateQuery<dynamic> (queryExpr);
        }

        public static IQueryable<TEntity> DynamicWhere<TEntity> (this IQueryable<TEntity> query, String searches) {
            if (string.IsNullOrWhiteSpace (searches)) {
                return query;
            }

            string[] searchArray = searches.Split (",");

            Expression queryExpr = query.Expression;
            ParameterExpression parameter = Expression.Parameter (query.ElementType, "p");
            Expression predicateBody = null;

            for (int i = 0; i < searchArray.Length; i++) {
                string searchValue = string.Empty;
                string searchField = string.Empty;
                string searchExpression = string.Empty;

                searchField = Regex.Split (searchArray[i], "\\[.*]") [0].UpperFirstLetter ();
                searchValue = Regex.Split (searchArray[i], "\\[.*]") [1];
                searchExpression = Regex.Replace (searchArray[i].Substring (searchField.Length, searchArray[i].IndexOf (searchValue) - searchField.Length), "[\\[\\]]", "");

                PropertyInfo property = query.ElementType.GetProperty (searchField);

                Expression left = Expression.Property (parameter, property);

                TypeConverter typeConverter = TypeDescriptor.GetConverter (property.PropertyType);
                object searchValueTyped = typeConverter.ConvertFromString (searchValue);

                Expression right = Expression.Constant (searchValueTyped, property.PropertyType);

                Expression expression = null;
                switch (searchExpression) {
                    case "=":
                        expression = Expression.Equal (left, right);
                        break;
                    case "!=":
                        expression = Expression.NotEqual (left, right);
                        break;
                    case ">=":
                        expression = Expression.GreaterThanOrEqual (left, right);
                        break;
                    case "<=":
                        expression = Expression.LessThanOrEqual (left, right);
                        break;
                    case ">":
                        expression = Expression.GreaterThan (left, right);
                        break;
                    case "<":
                        expression = Expression.LessThan (left, right);
                        break;
                    case "%":
                    case "!%":
                    case "^%":
                    case "%^":
                    case "!^%":
                    case "!%^":
                        expression = GetLikeExpressions (parameter, property, searchValue, searchExpression);
                        break;
                    default:
                        throw new Exception ("Undefined search parameter");
                }

                if (predicateBody == null) {
                    predicateBody = expression;
                } else {
                    predicateBody = Expression.And (predicateBody, expression);
                }
            }

            queryExpr = Expression.Call (
                typeof (Queryable),
                "Where",
                new Type[] { query.ElementType },
                queryExpr, Expression.Quote (Expression.Lambda (predicateBody, parameter))
            );

            return query.Provider.CreateQuery<TEntity> (queryExpr);
        }

        private static Expression GetLikeExpressions (ParameterExpression parameter, PropertyInfo property, string searchValue, string searchExpression) {
            Expression expression = null;
            MemberExpression memberExpression = Expression.MakeMemberAccess (parameter, property);
            ConstantExpression c = Expression.Constant (searchValue, typeof (string));
            MethodInfo mi = null;
            var toString = typeof (Object).GetMethod ("ToString");
            var toStringValue = Expression.Call (memberExpression, toString);

            switch (searchExpression) {
                case "%":
                    mi = typeof (string).GetMethod ("Contains", new Type[] { typeof (string) });
                    expression = Expression.Call (toStringValue, mi, c);

                    break;
                case "!%":
                    mi = typeof (string).GetMethod ("Contains", new Type[] { typeof (string) });
                    expression = Expression.Call (toStringValue, mi, c);
                    expression = Expression.Not (expression);

                    break;
                case "^%":
                    mi = typeof (string).GetMethod ("StartsWith", new Type[] { typeof (string) });
                    expression = Expression.Call (toStringValue, mi, c);
                    break;
                case "%^":
                    mi = typeof (string).GetMethod ("EndsWith", new Type[] { typeof (string) });
                    expression = Expression.Call (toStringValue, mi, c);
                    break;
                case "!^%":
                    mi = typeof (string).GetMethod ("StartsWith", new Type[] { typeof (string) });
                    expression = Expression.Call (toStringValue, mi, c);
                    expression = Expression.Not (expression);
                    break;
                case "!%^":
                    mi = typeof (string).GetMethod ("EndsWith", new Type[] { typeof (string) });
                    expression = Expression.Call (toStringValue, mi, c);
                    expression = Expression.Not (expression);
                    break;
            }

            return expression;
        }

        public static IQueryable<TEntity> DynamicOrder<TEntity> (this IQueryable<TEntity> query, String sorts) {
            if (string.IsNullOrWhiteSpace (sorts)) {
                return query;
            }

            string[] sortArray = sorts.Split (",");

            Expression queryExpr = query.Expression;

            for (int i = 0; i < sortArray.Length; i++) {
                string sortDirection = string.Empty;
                string sortField = string.Empty;

                if (sortArray[i][0] == '+') {
                    sortDirection = i == 0 ? "OrderBy" : "ThenBy";

                } else if (sortArray[i][0] == '-') {
                    sortDirection = i == 0 ? "OrderByDescending" : "ThenByDescending";
                } else {
                    //TODO throw undefined order error;
                }

                //TODO check validation
                sortField = sortArray[i].Substring (1, sortArray[i].Length - 1).UpperFirstLetter ();

                ParameterExpression parameter = Expression.Parameter (query.ElementType, "p");

                PropertyInfo property = query.ElementType.GetProperty (sortField);
                MemberExpression propertyAccess = Expression.MakeMemberAccess (parameter, property);

                queryExpr = Expression.Call (
                    typeof (Queryable),
                    sortDirection,
                    new Type[] { query.ElementType, property.PropertyType },
                    queryExpr, Expression.Quote (Expression.Lambda (propertyAccess, parameter))
                );
            }

            return query.Provider.CreateQuery<TEntity> (queryExpr);
        }
    }
}