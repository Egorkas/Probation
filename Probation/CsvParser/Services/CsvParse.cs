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
        public static List<Payment> CsvParseToPayment(string pathFile)
        {
            List<Payment> Payments = new List<Payment>();
            using(StreamReader sr = new StreamReader(pathFile, Encoding.Default))
            {

            }

            return Payments;
        }
        
        CsvSeparator Delimeter(string pathFile)
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
            var keyMaxCount = counts.FirstOrDefault(x => x.Value == counts.Values.Max()).Key;

            return keyMaxCount switch
            {
                ',' => CsvSeparator.Comma,
                ';' => CsvSeparator.Semicolon,
                '\t' => CsvSeparator.Tabulation,
                '|' => CsvSeparator.VSlash,
            };
        }
    }
}
