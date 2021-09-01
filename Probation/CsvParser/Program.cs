using System;
using System.Linq;
using CsvParser.Extensions;
using CsvParser.Services;

namespace CsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathCsv = @"f:\Probation\Probation\test.csv";
            string pathJson = @"f:\Probation\Probation\test.json";

            var list = JsonParse.ParseJson(pathJson, pathCsv);

            list.Display();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Name + ' ' + "Count of payment for this user is " + item.Payments.Count() + "\n" + "Count of order for this user is " + item.Orders.Count());
            //}
            Console.WriteLine("Hello World!");
        }
    }
}
