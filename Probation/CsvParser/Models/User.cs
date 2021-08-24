using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Models
{
    public class User
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Order> Orders { get; set; }
    }
}
