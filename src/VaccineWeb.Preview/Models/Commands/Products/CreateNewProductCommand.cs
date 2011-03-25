using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Commands;

namespace VaccineWeb.Preview.Models.Commands.Products
{
    public class CreateNewProductCommand : Command
    {
        public CreateNewProductCommand()
        {

        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}