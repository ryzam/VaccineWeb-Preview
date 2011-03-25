using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events.Orders;
using VaccineWeb.Preview.Models.Reports.Orders;

namespace VaccineWeb.Preview.Models.Domains.Orders
{
    public class Order : AggregateRoot<Order>, IRolePlayer
    {
        public IList<OrderLine> orderLines { get; private set; }

        public DateTime? orderdate;

        public Order()
        {
            RegisterEvent<NewOrderCreatedEvent>(OnNewOrderCreated);
            RegisterEvent<OrderLineAddedEvent>(OnOrderLineAdded);
        }

        public void CreateNewOrder(Guid customerId)
        {
            var e = new NewOrderCreatedEvent { CustomerAggregateRootId = customerId, OrderDate = DateTime.Now };
            Apply<NewOrderCreatedEvent>(e);
        }

        private void OnNewOrderCreated(NewOrderCreatedEvent e)
        {
            if (e.OrderDate.HasValue)
                this.orderdate = e.OrderDate;
        }

        public void AddOrderItem(int quantity, decimal totalAmount,string customerName, string productName, Guid productId, Guid customerOd)
        {
            var e = new OrderLineAddedEvent { Quantity = quantity, TotalAmount = totalAmount, ProductId = productId };
            Apply<OrderLineAddedEvent>(e)
                .SaveReport<CustomerOrdersDetailReport>(x =>
                    {
                        x.CustomerName = customerName;
                        x.ProductName = productName;
                        x.Quantity = quantity;
                        x.TotalAmount = totalAmount;
                        x.ProductId = productId;
                    }
            );
        }

        private void OnOrderLineAdded(OrderLineAddedEvent e)
        {
            if (orderLines == null)
                orderLines = new List<OrderLine>();
            orderLines.Add(new OrderLine(e.Quantity, e.TotalAmount));
        }

       
    }

    public class OrderLine
    {
        public int? quantity { get; private set; }
        public decimal? totalAmount { get; private set; }

        public OrderLine(int? quantity, decimal? totalAmount)
        {
            this.quantity = quantity;
            this.totalAmount = totalAmount;
        }

        
    }
}