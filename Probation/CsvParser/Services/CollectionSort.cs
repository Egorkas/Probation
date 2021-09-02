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
                    var list1 = SortForPaymentName(listForSort);
                    foreach (var item in list1)
                    {
                        Console.WriteLine(item.DeliveryTime);
                    }
                    SortForPayment(listForSort);
                    return OrderForPayments(listForSort, typeOfOrder);
                }
                else if (propForSort.Equals("Orders"))
                {
                   return OrderForOrders(listForSort, typeOfOrder);
                }

                return typeOfOrder == 0 ? listForSort.OrderBy(propForSort) : listForSort.OrderByDescending(propForSort);
            }

            WriteWithColor("This prop isn't exist!", ConsoleColor.DarkRed);
            return null;
        }

        private static IEnumerable<User> SortForPayment(IEnumerable<User> list)
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
                };
            }
            
            return list;
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
                    sortedList.AddRange(list.Where(x => x.Id == item.UserId));
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
                    foreach (var item in paymentList)
                    {
                        sortedList.AddRange(list.Where(x => x.Id == item.UserId));
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
