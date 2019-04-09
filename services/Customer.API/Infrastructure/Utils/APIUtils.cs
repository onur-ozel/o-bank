using System;
using System.Linq;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Infrastructure.Utils {
    public class APIUtils {
        public static void ParseSearches (string searches) {

        }
        public static IQueryable<RetailCustomer> ParseSort (String sorts, Contexts.CustomerContext context) {
            IQueryable<RetailCustomer> query = context.RetailCustomer;

            query.OrderBy("asd");
            // var firstPass = true;
            // foreach (var sortOrder in sortingPaging.SortOrders) {
            //     if (firstPass) {
            //         firstPass = false;
            //         query = sortOrder.ColumnOrder == SortOrderDto.SortOrder.Ascending ?
            //             query.OrderBy (sortOrder.ColumnName) :
            //             query.OrderByDescending (sortOrder.ColumnName);
            //     } else {
            //         query = sortOrder.ColumnOrder == SortOrderDto.SortOrder.Ascending ?
            //             query.ThenBy (sortOrder.ColumnName) :
            //             query.ThenByDescending (sortOrder.ColumnName);
            //     }
            // }

            return query;
        }
    }
}