using CsvParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvParser.Models
{
    class Payment
    {
        public string Name { get; set; }
        public PaymentType Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public TimeSpan DeliveryTime { get; set; }

    }
}
