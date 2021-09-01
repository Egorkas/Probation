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
            foreach (var item in list)
            {
                Console.WriteLine(item.Id + "\n" + item.Name + "\n" + item.SecondName);
                for (int i = 0; i < item.Payments.Count - 1; i++)
                {
                    Console.WriteLine("Payment Name - " + item.Payments[i].Name + "\n"
                        + "Payment Description - " + item.Payments[i].Description + "\n" 
                        + "Payment Type - " + (PaymentType)item.Payments[i].Type + "\n"
                        + "Payment Quantity - " + item.Payments[i].Quantity + "\n" 
                        + "Payment DeliveryTime - " + item.Payments[i].DeliveryTime);
                }

                for (int i = 0; i < item.Orders.Count - 1; i++)
                {
                    Console.WriteLine("Order Price - " + item.Orders[i].Price );
                }
            }
        }
    }
}
