using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Domains.Orders;
using Vaccine.Core;
using Vaccine.Core.Commands;
using Vaccine.Core.EventStorages;
using VaccineWeb.Preview.Models.Commands.Orders;
using VaccineWeb.Preview.Models.Domains.Customers;
using VaccineWeb.Preview.Models.Domains.Products;

namespace VaccineWeb.Preview.Models.RoleHandlers.Orders
{
    public class MakeNewOrderRole : PlayedBy<Order>
    {
        public override void Execute(ICommand cmd, IEventSourceRepository repository)
        {
            var c = cmd.MapToCommand<MakeNewOrderCommand>();

            var customer = repository.GetById<Customer>(c.CustomerId);

            Self.CreateNewOrder(customer.AggregateRootId);

            foreach (Guid item in c.Quantities.Keys)
            {
                var product = repository.GetById<Product>(item);

                var quantity = c.Quantities[item];

                var totalAmount = product.GetTotalAmount(quantity);

                if (customer.HasSufficientAmountToPurchase(totalAmount) && product.HasAvailableStockToPurchaseByQuantity(quantity))
                {
                    Self.AddOrderItem(quantity, totalAmount, customer.name,product.productName, product.AggregateRootId,customer.AggregateRootId);
                    customer.DecreaseCashBalance(totalAmount);
                    product.DecreaseStockBalance(quantity);
                }
                else
                {
                    throw new Exception("Insufficient amount to purchase");
                }
            }
        }
    }
}