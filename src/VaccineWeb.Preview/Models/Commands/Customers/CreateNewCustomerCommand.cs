using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core.Commands;

namespace VaccineWeb.Preview.Models.Commands.Customers
{
    public class CreateNewCustomerCommand : Command
    {
        public CreateNewCustomerCommand()
        {

        }

        public CreateNewCustomerCommand(string name,decimal cashBalance)
        {
            this.Name = name;
            this.CashBalance = cashBalance;
        }

        public string Name { get; set; }
        public decimal CashBalance { get; set; }
    }
}