using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineWeb.Preview.Models.Domains.Customers;
using Vaccine.Core;
using Vaccine.Core.Commands;
using Vaccine.Core.EventStorages;
using VaccineWeb.Preview.Models.Commands;
using VaccineWeb.Preview.Models.Commands.Customers;

namespace VaccineWeb.Preview.Models.RoleHandlers.Customers
{
    public class CreateNewCustomerRole : PlayedBy<Customer>
    {
        public override void Execute(ICommand cmd, IEventSourceRepository repository)
        {
            var c = cmd.MapToCommand<CreateNewCustomerCommand>();

            Self.CreateNewCustomer(c.Name, c.CashBalance);
        }
    }
}