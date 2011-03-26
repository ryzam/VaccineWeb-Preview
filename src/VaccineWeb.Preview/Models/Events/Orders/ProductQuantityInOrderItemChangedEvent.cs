using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Orders
{
    public class ProductQuantityInOrderItemChangedEvent : UpdateEvent
    {
        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }
    }
}