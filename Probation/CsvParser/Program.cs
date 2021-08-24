using System;

namespace CsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"f:\Probation\Probation\test.csv";
            var list = CsvParser.Services.CsvParse.CsvParseToPayment(path);

            foreach (var item in list)
            {
                Console.WriteLine(item.Name + ' ' + item.Description + ' ' + item.DeliveryTime.ToString());
            }
            Console.WriteLine("Hello World!");
        }
    }
}
