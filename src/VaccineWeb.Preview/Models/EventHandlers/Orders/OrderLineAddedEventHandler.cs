using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Events.Orders;
using Vaccine.Core.EventHandlers;
using VaccineWeb.Preview.Models.Reports.Orders;

namespace VaccineWeb.Preview.Models.EventHandlers.Orders
{
    public class OrderLineAddedEventHandler : IEventHandler<OrderLineAddedEvent>
    {
        public void Execute(OrderLineAddedEvent e)
        {
            var report = new CustomerOrdersDetailReport { AggregateRootId = e.AggregateRootId, CustomerName = e.CustomerName, ProductName = e.ProductName, ProductId = e.ProductId, Quantity = e.Quantity, TotalAmount = e.TotalAmount };

        }
    }
}