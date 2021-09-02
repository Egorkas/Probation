using CsvParser.Enums;
using CsvParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Extensions
{
    public static class DisplayUsersExtension
    {
        public static void Display(this IEnumerable<User> list)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            foreach (var item in list)
            {
                Console.WriteLine(item.Id + "\n" + item.Name + "\n" + item.SecondName);
                for (int i = 0; i < item.Payments.Count; i++)
                {
                    Console.WriteLine($"{i+1}.Payment Name - " + item.Payments[i].Name + "\n"
                        + $"{i+1}.Payment Description - " + item.Payments[i].Description + "\n" 
                        + $"{i+1}.Payment Type - " + (PaymentType)item.Payments[i].Type + "\n"
                        + $"{i+1}.Payment Quantity - " + item.Payments[i].Quantity + "\n" 
                        + $"{i+1}.Payment DeliveryTime - " + item.Payments[i].DeliveryTime);
                }

                for (int i = 0; i < item.Orders.Count; i++)
                {
                    Console.WriteLine($"{i+1}.Order Price - " + item.Orders[i].Price );
                }
            }
            Console.ResetColor();
        }
    }
}
