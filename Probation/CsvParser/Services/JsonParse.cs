using CsvParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvParser.Services
{
    public class JsonParse
    {
        public static List<User> ParseJson(string filePathJson, string filePathCsv)
        {
            if (!File.Exists(filePathJson))
            {
                Console.WriteLine($"File {filePathJson} doesn't exist!");
                return null;
            }
            var payments = CsvParser.Services.CsvParse.CsvParseToPayment(filePathCsv);

            var json = File.ReadAllText(filePathJson);
            var users = JsonConvert.DeserializeObject<List<User>>(json);

            foreach (var item in users)
            {
                item.Payments = payments.Where(c => c.UserId == item.Id).ToList();
            }
            
            return users;
        } 
    }
}
