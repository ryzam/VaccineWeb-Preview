using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Commands;

namespace VaccineWeb.Preview.Models.Commands.Orders
{
    public class ChangeProductQuantityInOrderItemCommand : Command
    {
        //ASP.Net MVC will use for binding
        public ChangeProductQuantityInOrderItemCommand()
        {

        }

        public Guid AggregateRootId { get; set; }
        public Guid RowId { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}