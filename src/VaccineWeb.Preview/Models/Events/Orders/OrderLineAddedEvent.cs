using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Orders
{
    public class OrderLineAddedEvent : CreateEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
    }
}