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
            string pathCsv = @"d:\Probation\Probation\test.csv";
            string pathJson = @"d:\Probation\Probation\test.json";

            var list = JsonParse.ParseJson(pathJson, pathCsv);

            list.Display();

            
            var sortedList = CollectionSort.ChoosePropForSort(list);
            sortedList.Display();
        }
    }
}
