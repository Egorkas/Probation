using CsvParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Models
{
    public class Payment
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public PaymentType Type { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } 
        public TimeSpan DeliveryTime { get; set; }

    }
}
