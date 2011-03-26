using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineWeb.Preview.Models.ViewModels;
using VaccineWeb.Preview.Models.Commands.Orders;
using VaccineWeb.Preview.Models.Domains.Orders;
using VaccineWeb.Preview.Models.RoleHandlers.Orders;
using Vaccine.Core;
using SisoDb;
using Vaccine.Core.EventStorages;
using VaccineWeb.Preview.Models.Reports.Orders;

namespace VaccineWeb.Preview.Controllers
{
    public class OrdersController : Controller
    {
        
        protected IUnitOfWork UnitOfWork;
        protected IEventSourceRepository repo;

        public OrdersController(IUnitOfWork uow, IEventSourceRepository repo)
        {
            this.UnitOfWork = uow;
            this.repo = repo;
                 
        }


        protected IList<CustomerOrderViewItemModel> viewItems { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MakeAnOrder()
        {
            var viewModel = new CustomerOrderViewModel();
            if (TempData["OrderItems"] != null)
            {
                viewModel.OrderItems = (List<CustomerOrderViewItemModel>)TempData["OrderItems"];
            }


            return View(viewModel);

        }

        [HttpPost]
        public ActionResult MakeAnOrder(CustomerOrderViewItemModel model)
        {
            var viewModel = new CustomerOrderViewModel();
            if (TempData["OrderItems"] == null)
                viewItems = new List<CustomerOrderViewItemModel>();
            else
                viewItems = (List<CustomerOrderViewItemModel>)TempData["OrderItems"];

            
            

            if (Request.Params["add"] != null)
            {
                viewItems.Add(model);
                TempData["OrderItems"] = viewItems;

                viewModel.OrderItems = viewItems;
                return View(viewModel);
            }
            else
            {
                TempData["OrderItems"] = viewItems;

                viewModel.OrderItems = viewItems;
                return MakeAnOrderComplete(viewModel);
            }
        }

        private ActionResult MakeAnOrderComplete(CustomerOrderViewModel model)
        {
            try
            {
                var cmd = new MakeNewOrderCommand { CustomerId = new Guid(Request.Params["CustomerId"]) };
                foreach (var item in model.OrderItems)
                {
                    cmd.Quantities.Add(item.ProductId, item.Quantity);
                }

                using (var s = UnitOfWork)
                {
                    var ctx = new ContextHandler<Order, MakeNewOrderRole>(repo);
                    ctx.Bind(cmd)
                       .Execute();
                    s.Commit();
                }

                return MakeAnOrder();
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        public ActionResult List()
        {
            var reports = ReadStorage.GetAll<CustomerOrdersDetailReport>();
            return View(reports);
        }

    
        public ActionResult Edit(Guid id, Guid rowId, Guid customerId, Guid productId)
        {
            try
            {
                using (var s = UnitOfWork)
                {
                    var cmd = new ChangeProductQuantityInOrderItemCommand { AggregateRootId = id, RowId = rowId, CustomerId = customerId, ProductId = productId, Quantity = 5 };
                    var ctx = new ContextHandler<Order, ChangeProductQuantityInOrderItemRole>(repo);
                    ctx.Bind(cmd)
                       .Execute();
                    s.Commit();
                }

            }
            catch (Exception err)
            {
            }
            return null;
        }
    }
}
