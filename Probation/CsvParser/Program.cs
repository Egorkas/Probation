using System;
using System.Linq;
using CsvParser.Services;

namespace CsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathCsv = @"d:\Probation\Probation\test.csv";
            string pathJson = @"d:\Probation\Probation\test.json";

            var list = JsonParse.ParseJson(pathJson, pathCsv);

            foreach (var item in list)
            {
                Console.WriteLine(item.Name + ' ' +"Count of payment for this user is "+ item.Payments.Count() + "\n" + "Count of order for this user is " + item.Orders.Count() );
            }
            Console.WriteLine("Hello World!");
        }
    }
}
