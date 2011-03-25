using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisoDb;
using Vaccine.Core.EventStorages;
using VaccineWeb.Preview.Models.Commands;
using VaccineWeb.Preview.Models.Domains.Customers;
using VaccineWeb.Preview.Models.RoleHandlers;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Commands.Customers;
using VaccineWeb.Preview.Models.RoleHandlers.Customers;
using VaccineWeb.Preview.Models.Reports.Customers;

namespace VaccineWeb.Preview.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/

        protected IUnitOfWork UnitOfWork;
        protected IEventSourceRepository repo;

        public CustomersController(IUnitOfWork uow,IEventSourceRepository repo)
        {
            this.UnitOfWork = uow;
            this.repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(CreateNewCustomerCommand cmd)
        {
            using (var s = UnitOfWork)
            {
                try
                {
                    ContextHandler<Customer, CreateNewCustomerRole> ctx = new ContextHandler<Customer, CreateNewCustomerRole>(repo);
                    ctx.Bind(cmd)
                       .Execute();
                    s.Commit();

                    return View();
                    
                }
                catch(Exception err)
                {
                    throw new Exception(err.Message);
                }
            }
        }

        public ActionResult List()
        {
            var reports = ReadStorage.GetAll<CustomerDetailReport>();
            return View(reports);
        }
    }
}
