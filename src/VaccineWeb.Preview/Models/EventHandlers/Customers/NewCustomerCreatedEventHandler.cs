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
    public class NewCustomerCreatedEventHandler : EventReportHandler, IEventHandler<NewCustomerCreatedEvent>
    {
        public void Execute(NewCustomerCreatedEvent e)
        {
            SaveReport<CustomerDetailReport>(x =>
                {
                    x.AggregateRootId = e.AggregateRootId;
                    x.name = e.Name;
                    x.cashBalance = e.CashBalance;
                }
            );

        }
    }
}