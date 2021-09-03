using CsvParser.Enums;
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
                    return SortForPayment(listForSort, typeOfOrder);
                }
                else if (propForSort.Equals("Orders"))
                {
                   return SortForOrder(listForSort, typeOfOrder);
                }

                return typeOfOrder == 0 ? listForSort.OrderBy(propForSort) : listForSort.OrderByDescending(propForSort);
            }

            WriteWithColor("This prop isn't exist!", ConsoleColor.DarkRed);
            return listForSort;
        }

        private static IEnumerable<User> SortForPayment(IEnumerable<User> list, int typeOfOrder)
        {
            WriteWithColor("User.Paiment has such property for sorting:", ConsoleColor.DarkGray);
            var prop = typeof(Payment)
               .GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            for (int i = 1; i < prop.Count(); i++)
            {
                WriteWithColor(prop.ElementAt(i).Name.ToString(), ConsoleColor.DarkRed);
            }

            Console.WriteLine("Choose property (enter full name of Property)");
            var propForSort = Console.ReadLine();
            if (prop.Any(x => x.Name == propForSort))
            {
                return propForSort switch
                {
                    "Name" => SortForPaymentName(list),
                    "Type" => SortForPaymentType(list),
                    "DeliveryTime" => SortForPaymentTime(list),
                    _ => OrderForPayments(list, typeOfOrder)
                };
            }

            WriteWithColor("Your property doesn't exist!", ConsoleColor.DarkRed);
            return list;
        }

        private static IEnumerable<User> SortForOrder(IEnumerable<User> list, int typeOfOrder)
        {
            WriteWithColor("User.Paiment has such property for sorting: \n 1. MaxSum \n 2.Price Equal \n 3.OrderBy Count", ConsoleColor.DarkCyan);
            WriteWithColor("Choose property (enter value, like: 1 for MaxSum)", ConsoleColor.DarkGreen);
            var type = 0;
            if (Int32.TryParse(Console.ReadLine(), out type))
            {
                return type switch
                {
                    1 => SortForOrderMaxSum(list),
                    2 => SortForOrderPriceEqual(list),
                    _ => OrderForOrders(list, typeOfOrder)
                };
            }

            WriteWithColor("Your property doesn't exist!", ConsoleColor.DarkRed);
            return list;
        }

        private static IEnumerable<User> SortForOrderPriceEqual(IEnumerable<User> list)
        {
            var sortedList = new List<User>();
            var orderList = GetOrders(list);
            WriteWithColor("Enter yout price:", ConsoleColor.DarkGreen);
            decimal price = default;
            if (!Decimal.TryParse(Console.ReadLine(), out price))
            {
                WriteWithColor("You enter no valid price!", ConsoleColor.DarkRed);
                return list;
            }

            if (orderList.Any(x => x.Price == price))
            {
                foreach (var item in orderList)
                {
                    sortedList.AddRange(list.Where(x => item.Price >= price && x.Id == item.UserId));
                }
                return sortedList;
            }

            WriteWithColor("There are no elements equal to your price!", ConsoleColor.DarkRed);
            return list;
        }

        private static IEnumerable<User> SortForOrderMaxSum(IEnumerable<User> list)
        {
            var ordersList = GetOrders(list);
            var sortedList = new List<User>();
            try
            {
                sortedList.Add(list.OrderByDescending(x => x.Orders.Sum(n => n.Price)).First());
                return sortedList;
            }
            catch
            {
                WriteWithColor("Collection hasn't any orders!", ConsoleColor.DarkRed);
                return list;
            }
        }

        private static IEnumerable<User> SortForPaymentName(IEnumerable<User> list)
        {
            var sortedList = new List<User>();
            var paymentList = GetPayments(list);

            WriteWithColor("Enter the Name: ", ConsoleColor.Red);
            var name = Console.ReadLine();
            if (paymentList.Any(x => x.Name == name))
            {
                foreach (var item in paymentList)
                {
                    sortedList.AddRange(list.Where(x => item.Name == name && x.Id == item.UserId));
                }
                return sortedList;
            }

            WriteWithColor("Tis Name isn't exist in this collection!", ConsoleColor.DarkRed);    
            return list;
        }

        private static IEnumerable<User> SortForPaymentType(IEnumerable<User> list)
        {
            var sortedList = new List<User>();
            var paymentList = GetPayments(list);
            foreach (var item in Enum.GetNames(typeof(PaymentType)))
            {
                WriteWithColor(item, ConsoleColor.DarkCyan);
            }
            WriteWithColor("Enter the name of Type: ", ConsoleColor.Red);
            try
            {
                var type = (PaymentType)Enum.Parse(typeof(PaymentType), Console.ReadLine());
                if (paymentList.Any(x => x.Type == type))
                {
                    //var 
                    foreach (var item in paymentList)
                    {
                        sortedList.AddRange(list.Where(x => item.Type == type && x.Id == item.UserId));
                    }
                    return sortedList;
                }
                WriteWithColor("Tis Name isn't exist in this collection!", ConsoleColor.DarkRed);
                return list;
            }
            catch
            {
                WriteWithColor("This type isn't exist!", ConsoleColor.DarkRed);
                return list;
            }
        }

        private static IEnumerable<User> SortForPaymentTime(IEnumerable<User> list)
        {
            var sortedList = new List<User>();
            var paymentList = GetPayments(list);

            WriteWithColor("Enter the  max Time(number of hours): ", ConsoleColor.Red);
            TimeSpan time = default;
            if(!TimeSpan.TryParse(Console.ReadLine(), out time))
            {
                WriteWithColor("You enter no valid time!", ConsoleColor.DarkRed);
                return list;
            }

            if (paymentList.Any(x => x.DeliveryTime >= time))
            {
                foreach (var item in paymentList)
                {
                    sortedList.AddRange(list.Where(x => item.DeliveryTime >= time && x.Id == item.UserId));
                }
                return sortedList;
            }

            WriteWithColor("There are no elements less than your time!", ConsoleColor.DarkRed);
            return list;
        }
        private static IEnumerable<Payment> GetPayments(IEnumerable<User> list)
        {
            var paymentList = new List<Payment>();
            foreach (var item in list)
            {
                paymentList.AddRange(item.Payments);
            }
            return paymentList;
        }

        private static IEnumerable<Order> GetOrders(IEnumerable<User> list)
        {
            var orderList = new List<Order>();
            foreach (var item in list)
            {
                orderList.AddRange(item.Orders);
            }
            return orderList;
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
