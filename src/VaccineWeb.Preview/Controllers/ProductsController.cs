using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VaccineWeb.Preview.Models.Commands.Products;
using VaccineWeb.Preview.Models.Domains.Products;
using VaccineWeb.Preview.Models.RoleHandlers.Products;
using Vaccine.Core;
using Vaccine.Core.EventStorages;
using SisoDb;
using VaccineWeb.Preview.Models.Reports.Products;

namespace VaccineWeb.Preview.Controllers
{
    public class ProductsController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        protected IEventSourceRepository repo;

        public ProductsController(IUnitOfWork uow,IEventSourceRepository repo)
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
        public ActionResult Register(CreateNewProductCommand cmd)
        {
            try
            {
                using (var s = UnitOfWork)
                {
                    ContextHandler<Product, CreateNewProductRole> ctx = new ContextHandler<Product, CreateNewProductRole>(repo);
                    ctx.Bind(cmd)
                       .Execute();
                    s.Commit();

                    return View();
                }
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public ActionResult List()
        {
            var reports = ReadStorage.GetAll<ProductDetailReport>();
            return View(reports);
        }

    }
}
