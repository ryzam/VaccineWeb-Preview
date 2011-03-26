using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events.Orders;
using VaccineWeb.Preview.Models.Reports.Orders;
using Vaccine.Core.Infrastructure;

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
            RegisterEvent<ProductQuantityInOrderItemChangedEvent>(OnProductQuantityInOrderItemChanged);
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

        public void AddOrderItem(int quantity, decimal totalAmount,string customerName, string productName, Guid productId, Guid customerId)
        {
           
            var e = new OrderLineAddedEvent { Quantity = quantity, TotalAmount = totalAmount, ProductId = productId };
            Apply<OrderLineAddedEvent>(e)
                    .Save<CustomerOrdersDetailReport>(x =>
                    {
                        x.AggregateRootId = this.AggregateRootId;
                        x.RowId = e.RowId;
                        x.CustomerName = customerName;
                        x.ProductName = productName;
                        x.Quantity = quantity;
                        x.TotalAmount = totalAmount;
                        x.ProductId = productId;
                        x.CustomerId = customerId;
                    }
            );

 
        }

        private void OnOrderLineAdded(OrderLineAddedEvent e)
        {
            if (orderLines == null)
                orderLines = new List<OrderLine>();
            orderLines.Add(new OrderLine(e.RowId,e.Quantity, e.TotalAmount));
        }

        public int GetQuantityInSelectedOrderItem(Guid rowId)
        {
            return orderLines.Where(c => c.RowId == rowId).FirstOrDefault().quantity;
        }

        public decimal GetTotalAmountInSelectedOrderItem(Guid rowId)
        {
            return orderLines.Where(c => c.RowId == rowId).FirstOrDefault().totalAmount;
        }

        public void ChangeProductQuantityInOrderItem(Guid rowId, int quantity, decimal newtotalAmount)
        {
            var e = new ProductQuantityInOrderItemChangedEvent { RowId = rowId, Quantity = quantity, TotalAmount = newtotalAmount };
            Apply<ProductQuantityInOrderItemChangedEvent>(e)
                .Update<CustomerOrdersDetailReport>(x =>
                {
                    x.AggregateRootId = this.AggregateRootId;
                    x.RowId = rowId;
                    x.TotalAmount = newtotalAmount;
                    x.Quantity = quantity;
                });
        }

        private void OnProductQuantityInOrderItemChanged(ProductQuantityInOrderItemChangedEvent e)
        {
            var orderItem = orderLines.Where(c => c.RowId == e.RowId).FirstOrDefault();
            orderItem.ChangeProductQuantity(e.Quantity, e.TotalAmount);
        }
    }

    public class OrderLine : Domain
    {

        public OrderLine()
        {
            //RegisterEvent<OrderLineAddedEvent>(OnOrderLineAdded);
        }

        public int quantity { get; private set; }
        public decimal totalAmount { get; private set; }
       

        public OrderLine(Guid rowId,int quantity, decimal totalAmount)
            : this()
        {
            this.RowId = rowId;
            this.quantity = quantity;
            this.totalAmount = totalAmount;
        }


        public void ChangeProductQuantity(int quantity, decimal newtotalAmount)
        {
            this.quantity = quantity;
            this.totalAmount = newtotalAmount;
           
        }
    }
}