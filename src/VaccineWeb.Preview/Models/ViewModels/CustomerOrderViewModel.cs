using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Domains.Products;
using VaccineWeb.Preview.Models.Domains.Customers;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Reports.Products;
using VaccineWeb.Preview.Models.Reports.Customers;

namespace VaccineWeb.Preview.Models.ViewModels
{
    public class CustomerOrderViewModel
    {
        public CustomerOrderViewModel()
        {
            Products = ReadStorage.GetAll<ProductDetailReport>();
            Customers = ReadStorage.GetAll<CustomerDetailReport>();
        }
        public IEnumerable<ProductDetailReport> Products { get; set; }

        public Guid ProductId { get; set; }

        public IEnumerable<CustomerDetailReport> Customers { get; set; }

        public IList<CustomerOrderViewItemModel> OrderItems { get; set; }

        public Guid CustomerId { get; set; }

        public int Quantity { get; set; }
    }

    public class CustomerOrderViewItemModel
    {
        public Guid ProductId { get; set; }

        private string _productName;

        public string ProductName
        {
            get
            {
                return ReadStorage.GetById<ProductDetailReport>(ProductId).ProductName;
            }
            set
            {
                _productName = value;
            }
        }

        public Guid CustomerId { get; set; }

        private string _name;
        public string Name
        {
            get
            {
                return ReadStorage.GetById<CustomerDetailReport>(CustomerId).name;
            }
            set
            {
                _name = value;
            }
        }

        public int Quantity { get; set; }

    }

}