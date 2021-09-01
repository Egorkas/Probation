using CsvParser.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CsvParser.Services
{
    public class CollectionSort
    {
        private static List<PropertyInfo> _properties;
        public static IEnumerable<PropertyInfo> GetProperty<T>(List<T>  list)
        {
            if (list == null)
            {
                Console.WriteLine($"List {list} is null.");
                return null;
            }
            
            var propList = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            return propList;
            //var listOfGenProp = propertiesOfModel
            //    .Where(x => x.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(x.PropertyType));
        }

        public static void ChoosePropForSort<T>(List<T> listForSort)
        {
            Console.WriteLine("Collectin has such property for sorting:");

        }


    }
}
