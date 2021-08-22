using CsvParser.Enums;
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
        private char _delimeter;
        public static List<Payment> CsvParseToPayment(string pathFile)
        {
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
                    //if (Try)
                }
            }

            return Payments;
        }

        static bool TryParseRow(string lineRow)
        {
            var payment = new Payment();
            try
            {
                var columns = lineRow.Split(_delimeter);

                if(columns.Count() == 5)
                {
                    payment.Name = columns[0];
                    payment.Type = (PaymentType)Int32.Parse(columns[1]);
                    payment.Description = columns[2];
                    payment.Quantity = Int32.Parse(columns[3]);
                    payment.DeliveryTime = TimeSpan.Parse(columns[4]);
                    return true;
                }
                return false;
            }catch (Exception ex)
            {
                return false;
            }
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
