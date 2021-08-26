using CsvParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvParser.Services
{
    public class JsonParse
    {
        public static List<User> ParseJson(string filePathJson)
        {
            if (!File.Exists(filePathJson))
            {
                Console.WriteLine($"File {filePathJson} doesn't exist!");
                return null;
            }
            //var users = new List<User>();
            var json = File.ReadAllText(filePathJson);
            var users = JsonConvert.DeserializeObject<List<User>>(json);
            
            return users;
        } 
    }
}
