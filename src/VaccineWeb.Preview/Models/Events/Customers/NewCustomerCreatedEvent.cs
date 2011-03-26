using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Customers
{
    public class NewCustomerCreatedEvent : AggregateCreateEvent
    {
        public string Name { get; set; }
        public decimal CashBalance { get; set; }

      
        public NewCustomerCreatedEvent(string name, decimal cashBalance):base()
        {
            // TODO: Complete member initialization
            this.Name = name;
            this.CashBalance = cashBalance;
        }
    }
}