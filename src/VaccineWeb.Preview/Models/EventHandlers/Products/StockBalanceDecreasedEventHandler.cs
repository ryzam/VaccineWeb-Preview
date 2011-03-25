using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events.Products;
using Vaccine.Core.EventHandlers;
using VaccineWeb.Preview.Models.Reports.Products;

namespace VaccineWeb.Preview.Models.EventHandlers.Products
{
    public class StockBalanceDecreasedEventHandler : IEventHandler<StockBalanceDecreasedEvent>
    {
        public void Execute(StockBalanceDecreasedEvent e)
        {
            ReadStorage.Update<ProductDetailReport>(new ProductDetailReport { AggregateRootId = e.AggregateRootId, Stock = e.Stock });
        }
    }
}