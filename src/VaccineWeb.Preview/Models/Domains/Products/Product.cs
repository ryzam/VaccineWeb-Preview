using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events.Products;
using VaccineWeb.Preview.Models.Reports.Products;

namespace VaccineWeb.Preview.Models.Domains.Products
{
    public class Product : AggregateRoot<Product>, IRolePlayer
    {
        public Product()
        {
            RegisterEvent<NewProductCreatedEvent>(OnNewProductCreated);
            RegisterEvent<StockBalanceDecreasedEvent>(OnStockBalanceDecreased);
        }

        public string productName { get; private set; }
        public decimal price { get; private set; }
        public int stock { get; private set; }

        public void CreateNewProduct(string productName, decimal price, int stock)
        {
            var e = new NewProductCreatedEvent(productName, price, stock);
            Apply<NewProductCreatedEvent>(e)
                .SaveReport<ProductDetailReport>(x =>
                    {
                        x.ProductName = productName;
                        x.Price = price;
                        x.Stock = stock;
                    }
            );
        }

        private void OnNewProductCreated(NewProductCreatedEvent e)
        {
            if (e.ProductName != null)
                this.productName = e.ProductName;
            
                this.price = e.Price;
            
                this.stock = e.Stock;
        }

        public decimal GetTotalAmount(int quantity)
        {
            return price * quantity;
        }

        public bool HasAvailableStockToPurchaseByQuantity(int quantity)
        {
            if (stock > quantity)
                return true;
            return false;
        }

        public void DecreaseStockBalance(int quantity)
        {
            if(stock>=quantity)
                this.stock -= quantity;

            var e = new StockBalanceDecreasedEvent { Stock = stock };
            Apply<StockBalanceDecreasedEvent>(e)
                .UpdateReport<ProductDetailReport>(x => x.Stock = stock);
        }

        private void OnStockBalanceDecreased(StockBalanceDecreasedEvent e)
        {
                this.stock = e.Stock;
        }
    }
}