using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Commands;

namespace VaccineWeb.Preview.Models.Commands.Orders
{
    public class MakeNewOrderCommand :Command
    {
        public MakeNewOrderCommand()
        {
            if (Quantities == null)
                Quantities = new Dictionary<Guid, int>();
        }
        public Guid CustomerId { get; set; }

        public Dictionary<Guid,int> Quantities { get; set; }
    }
}