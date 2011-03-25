using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Customers
{
    public class NewCustomerCreatedEvent : CreateEvent
    {
        public string Name { get; set; }
        public decimal CashBalance { get; set; }

        public NewCustomerCreatedEvent()
        {

        }

        public NewCustomerCreatedEvent(string name, decimal cashBalance)
        {
            // TODO: Complete member initialization
            this.Name = name;
            this.CashBalance = cashBalance;
        }
    }
}