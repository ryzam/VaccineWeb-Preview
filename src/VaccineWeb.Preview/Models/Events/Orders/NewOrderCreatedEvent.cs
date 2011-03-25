using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Orders
{
    public class NewOrderCreatedEvent : CreateEvent
    {
        public Guid CustomerAggregateRootId { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}