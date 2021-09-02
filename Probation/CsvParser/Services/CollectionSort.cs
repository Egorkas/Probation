using CsvParser.Extensions;
using CsvParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CsvParser.Services
{
    public class CollectionSort
    {
        private static List<PropertyInfo> _genProperties;
        private static IEnumerable<PropertyInfo> GetProperty<T>(IEnumerable<T> list)
        {
            if (list == null)
            {
                Console.WriteLine($"List {list} is null.");
                return null;
            }

            var propList = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            _genProperties = propList
                .Where(x => x.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(x.PropertyType)).ToList();
            return propList;

        }

        public static IEnumerable<User> ChoosePropForSort(IEnumerable<User> listForSort)
        {
            Console.WriteLine("Collectin has such property for sorting:");
            var prop = CollectionSort.GetProperty<User>(listForSort);
            if (prop == null)
            {
                return null;
            }

            foreach (var item in prop)
            {
                WriteWithColor(item.Name, ConsoleColor.DarkGreen);
            }
            Console.WriteLine("Choose property (enter full name of Property)");
            var propForSort = Console.ReadLine();
            if (prop.Any(x => x.Name == propForSort))
            {
                Console.WriteLine("Enter type of order" + "\n"
                    + "0 - id direct" + "\n"
                    + "1 - if reverse");

                var typeOfOrder = 0;
                Int32.TryParse(Console.ReadLine(), out typeOfOrder);

                if (propForSort.Equals("Payments"))
                {
                    return OrderForPayments(listForSort, typeOfOrder);
                }
                else if (propForSort.Equals("Orders"))
                {
                   return OrderForOrders(listForSort, typeOfOrder);
                }

                return typeOfOrder == 0 ? listForSort.OrderBy(propForSort) :listForSort.OrderByDescending(propForSort);
            }
            return null;
        }

        private static IEnumerable<User> OrderForPayments(IEnumerable<User> list, int order)
        {
            return order == 0 ? list.OrderBy(x => x.Payments.Count()) : list.OrderByDescending(x  => x.Payments.Count());
        }
        private static IEnumerable<User> OrderForOrders(IEnumerable<User> list, int order)
        {
            return order == 0 ? list.OrderBy(x => x.Orders.Count()) : list.OrderByDescending(x => x.Orders.Count());
        }

        public static void WriteWithColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }


    }
}
