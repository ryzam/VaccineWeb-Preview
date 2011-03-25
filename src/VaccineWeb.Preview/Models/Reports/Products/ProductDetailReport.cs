using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;

namespace VaccineWeb.Preview.Models.Reports.Products
{
    public class ProductDetailReport : Report
    {

        public ProductDetailReport()
        {

        }

        public ProductDetailReport(Guid id, string productName, decimal? price, int? stock)
        {
            // TODO: Complete member initialization
            this.AggregateRootId = id;
            this.ProductName = productName;
            this.Price = price;
            this.Stock = stock;
        }
       
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}