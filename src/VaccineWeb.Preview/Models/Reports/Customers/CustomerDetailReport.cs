using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;

namespace VaccineWeb.Preview.Models.Reports.Customers
{
    public class CustomerDetailReport : Report
    {
        public string name { get; set; }
        public decimal cashBalance { get; set; }

        public CustomerDetailReport()
        {

        }

        public CustomerDetailReport(Guid customerId, string name, decimal cashBalance)
        {
            // TODO: Complete member initialization
            this.AggregateRootId = customerId;
            this.name = name;
            this.cashBalance = cashBalance;
        }

       
    }
}