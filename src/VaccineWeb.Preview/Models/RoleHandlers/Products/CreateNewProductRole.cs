using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Domains.Products;
using Vaccine.Core;
using Vaccine.Core.Commands;
using Vaccine.Core.EventStorages;
using VaccineWeb.Preview.Models.Commands.Products;

namespace VaccineWeb.Preview.Models.RoleHandlers.Products
{
    public class CreateNewProductRole : PlayedBy<Product>
    {
        public override void Execute(ICommand cmd, IEventSourceRepository repository)
        {
            var c = cmd.MapToCommand<CreateNewProductCommand>();

            Self.CreateNewProduct(c.ProductName, c.Price, c.Stock);
        }  
    }
}