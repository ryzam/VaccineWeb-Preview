using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Events;
using Vaccine.Core.EventHandlers;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events.Customers;
using VaccineWeb.Preview.Models.Reports.Customers;

namespace VaccineWeb.Preview.Models.EventHandlers.Customers
{
    public class NewCustomerCreatedEventHandler : IEventHandler<NewCustomerCreatedEvent>
    {
        public void Execute(NewCustomerCreatedEvent e)
        {
            ReadStorage.Save<CustomerDetailReport>(new CustomerDetailReport(e.AggregateRootId, e.Name, e.CashBalance));
        }
    }
}