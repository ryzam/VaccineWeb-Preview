﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Customers
{
    public class CashBalanceDecreasedEvent : UpdateEvent
    {
        public decimal CashBalance { get; set; }
    }
}