using CsvParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CsvParser.Services
{
    public class CollectionSort
    {
        private List<PropertyInfo> _properties;
        public void DisplayPropertyForSort()
        {
            var properiesOfUser = typeof(User)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

            foreach (var item in properiesOfUser)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("Enter the name of property");
            var propertyName = Console.ReadLine();
            if (!properiesOfUser.Exists(x => x.Name == propertyName))
            {
                Console.WriteLine("This property isn't exist!");
            }
            //list = list.OrderBy(propertyName).ToList();
        }
    }
}
