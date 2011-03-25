using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Vaccine.Core.EventHandlers;
using Autofac;
using System.Configuration;
using Vaccine.Core.EventStorages;
using System.Reflection;
using Autofac.Integration.Mvc;
using VaccineWeb.Preview.Models.Events;

using VaccineWeb.Preview.Models.Events.Customers;

using VaccineWeb.Preview.Models.Events.Products;

using VaccineWeb.Preview.Models.Events.Orders;
using Vaccine.Core;
using VaccineWeb.Preview.Models.EventHandlers.Customers;


namespace VaccineWeb.Preview
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected IContainer container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var connection = ConfigurationManager.AppSettings["conn"];
            ReadStorage rs = new ReadStorage(connection);
            AddEventHandler();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new ComponentBootStrapper(connection));
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        
        //Add EventHandler here if dont want to use SaveReport and UpdateReport in AggregateRoot
        protected void AddEventHandler()
        {
            //RegisterEventHandler.AddEventHandler<NewCustomerCreatedEvent>(e => new NewCustomerCreatedEventHandler().Execute(e));
            //RegisterEventHandler.AddEventHandler<CashBalanceDecreasedEvent>(e => new CashBalanceDecreasedEventHandler().Execute(e));
        }
       
    }
}