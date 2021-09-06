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

                if (columns.Count() == 6)
                {
                    payment.UserId = int.Parse(columns[0]);
                    payment.Name = columns[1];
                    payment.Type = (PaymentType)int.Parse(columns[2]);
                    payment.Description = columns[3];
                    payment.Quantity = Int32.Parse(columns[4]);
                    payment.DeliveryTime = TimeSpan.Parse(columns[5]);
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
