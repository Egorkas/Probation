using CsvParser.Enums;
using CsvParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvParser.Extensions
{
    public static class PaymentExtension
    {
        public static bool TryParseRow(this Payment payment, string lineRow,char delimeter)
        {
            try
            {
                var columns = lineRow.Split(delimeter);

                if (columns.Count() == 5)
                {
                    payment.Name = columns[0];
                    payment.Type = (PaymentType)int.Parse(columns[1]);
                    payment.Description = columns[2];
                    payment.Quantity = Int32.Parse(columns[3]);
                    payment.DeliveryTime = TimeSpan.Parse(columns[4]);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
