using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nameless.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Nameless.WebApi.Repositories;

namespace Nameless.WebApi.Utils
{
    public static class ControllerUtils
    {
        public static string ToFilterString<T>(this string query)
        {
            var typeEntity = typeof(T);
            String strBoolOperator = String.Empty;
            StringBuilder filter = new StringBuilder();

            foreach (PropertyInfo property in typeEntity.GetProperties())
            {
                if (query != null && property.PropertyType == typeof(string))
                {
                    filter.Append(property.BuildCondition(query, strBoolOperator));
                    strBoolOperator = " ||";
                }
            }
            return filter.ToString();
        }

        public static string BuildCondition(this PropertyInfo property, string strValue, string strBoolOperator = "")
        {
            string strCondition;
            strValue = String.Format("\"{0}\"", strValue);
            if (strValue != null && strValue.StartsWith("*") && strValue.EndsWith("*"))
                strCondition = String.Format("{0} a.{1}.Contains({2}, System.StringComparison.OrdinalIgnoreCase)", strBoolOperator, property.Name, strValue.Substring(1).Substring(0, strValue.Length - 1));
            else if (strValue != null && strValue.StartsWith("*"))
                strCondition = String.Format("{0} a.{1}.EndsWith({2}, System.StringComparison.OrdinalIgnoreCase)", strBoolOperator, property.Name, strValue.Substring(1));
            else if (strValue != null && strValue.EndsWith("*"))
                strCondition = String.Format("{0} a.{1}.StartsWith({2}, System.StringComparison.OrdinalIgnoreCase)", strBoolOperator, property.Name, strValue.Substring(0, strValue.Length - 1));
            else
                strCondition = String.Format("{0} a.{1}.Contains({2}, System.StringComparison.OrdinalIgnoreCase)", strBoolOperator, property.Name, strValue);
            return strCondition;
        }


        public static async Task<IEnumerable<T>> OrderBy<T>(this IEnumerable<T> values, string orderField, OrderType orderType) where T : class
        {
            string orden = "o=> o." + orderField;
            var options = ScriptOptions.Default.AddReferences(typeof(T).Assembly);
            Func<T, object> expression = await CSharpScript.EvaluateAsync<Func<T, object>>(orden, options);
            if (orderType == OrderType.Asc)
                values = values.OrderBy(expression);
            else
                values = values.OrderByDescending(expression);
            return values;
        }

        public static IEnumerable<T> Limit<T>(this IEnumerable<T> values, int pageIndex, int pageSize) where T : class
        {
            if (pageIndex > 0)
                values = values.Skip((pageIndex - 1) * pageSize);
            else
                throw new ArgumentOutOfRangeException("pageIndex must be greater than 0");

            if (pageSize > 0)
                values = values.Take(pageSize);
            else
                throw new ArgumentOutOfRangeException("pageSize must be greater than 0");
            return values;
        }

        public static OrderRequest GetOrderRequest(this Type tp, string? orderField, OrderType? orderType)
        {
            string[] fieldNames = tp.GetProperties().Select(x => x.Name).ToArray();

            if (orderField == null && fieldNames.Contains("Id"))
                orderField = "Id";
            else if (orderField == null)
                orderField = fieldNames.FirstOrDefault();

            if (!fieldNames.Contains("Id"))
                throw new ArgumentException($"'{orderField}' is not a field from the entity '{tp.Name}' ");

            if (orderType == null)
                orderType = OrderType.Asc;

            return new OrderRequest() { orderField = orderField, orderType = orderType.Value };
        }


    }
}
