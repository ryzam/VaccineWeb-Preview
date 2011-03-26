using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;

namespace VaccineWeb.Preview.Models.Reports.Orders
{
    public class CustomerOrdersDetailReport : Report
    {

        public string CustomerName { get; set; }

        public Guid CustomerId { get; set; }

        public string ProductName { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }
    }
}