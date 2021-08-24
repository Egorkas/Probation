using CsvParser.Enums;
using CsvParser.Extensions;
using CsvParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvParser.Services
{
    class CsvParse
    {
        private static char _delimeter;
        public static List<Payment> CsvParseToPayment(string pathFile)
        {
            List<Payment> payments = new List<Payment>();
            if (!File.Exists(pathFile))
            {
                Console.WriteLine($"File {pathFile} doesn't exist!");
                return null;
            }
            List<Payment> Payments = new List<Payment>();
            using(StreamReader sr = new StreamReader(pathFile, Encoding.Default))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var payment = new Payment();
                    if (payment.TryParseRow(line, _delimeter))
                    {
                        payments.Add(payment);
                    }
                }
            }

            return Payments;
        }

        void Delimeter(string pathFile)
        {
            string line = String.Empty;
            List<char> delimeters = new List<char> { ',', ';', '\t', '|'};
            Dictionary<char, int> counts = delimeters.ToDictionary(key => key, value => 0);
            
            using(StreamReader srForCheck = new StreamReader(pathFile, Encoding.Default))
            {
                line = srForCheck.ReadLine();
            }

            foreach (char item in delimeters)
            {
                counts[item] = line.Count(c => c == item);
            }
            _delimeter = counts.FirstOrDefault(x => x.Value == counts.Values.Max()).Key;
        }
    }
}
