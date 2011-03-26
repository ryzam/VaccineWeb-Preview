using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Events.Products
{
    public class NewProductCreatedEvent : AggregateCreateEvent
    {
       
        public NewProductCreatedEvent(string productName, decimal price, int stock):base()
        {
            this.ProductName = productName;
            this.Price = price;
            this.Stock = stock;
        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}