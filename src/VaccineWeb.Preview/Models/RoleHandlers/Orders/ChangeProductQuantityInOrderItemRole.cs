using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Domains.Orders;
using Vaccine.Core;
using Vaccine.Core.Commands;
using Vaccine.Core.EventStorages;
using VaccineWeb.Preview.Models.Commands.Orders;
using VaccineWeb.Preview.Models.Domains.Products;
using VaccineWeb.Preview.Models.Domains.Customers;

namespace VaccineWeb.Preview.Models.RoleHandlers.Orders
{
    public class ChangeProductQuantityInOrderItemRole : PlayedBy<Order>
    {
        public override void Execute(ICommand cmd, IEventSourceRepository repository)
        {
            var c = cmd.MapToCommand<ChangeProductQuantityInOrderItemCommand>();

            //Arrange
            var order = repository.GetById<Order>(c.AggregateRootId);

            var product = repository.GetById<Product>(c.ProductId);

            var customer = repository.GetById<Customer>(c.CustomerId);

            var quantity = order.GetQuantityInSelectedOrderItem(c.RowId);

            var totalAmount = order.GetTotalAmountInSelectedOrderItem(c.RowId);

            var newtotalAmount = product.GetTotalAmount(c.Quantity);

            //Act
            customer.IncreaseCashBalance(totalAmount);

            product.IncreaseStockBalance(quantity);
            
            order.ChangeProductQuantityInOrderItem(c.RowId, c.Quantity, newtotalAmount);

            customer.DecreaseCashBalance(newtotalAmount);

            product.DecreaseStockBalance(c.Quantity);

        }
    }
}