using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Events.Products;
using Vaccine.Core.EventHandlers;
using VaccineWeb.Preview.Models.Reports.Products;
using Vaccine.Core;

namespace VaccineWeb.Preview.Models.EventHandlers.Products
{
    public class NewProductCreatedEventHandler : IEventHandler<NewProductCreatedEvent>
    {
        public void Execute(NewProductCreatedEvent e)
        {
            var report = new ProductDetailReport(e.AggregateRootId, e.ProductName, e.Price, e.Stock);
            ReadStorage.Save<ProductDetailReport>(report);
        }
    }
}