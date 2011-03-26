﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Products
{
    public class StockBalanceDecreasedEvent : AggregateUpdateEvent
    {
        public int Stock { get; set; }
    }
}