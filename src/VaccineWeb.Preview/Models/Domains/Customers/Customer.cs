using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events;
using VaccineWeb.Preview.Models.Events.Customers;
using VaccineWeb.Preview.Models.Reports.Customers;

namespace VaccineWeb.Preview.Models.Domains.Customers
{
    public class Customer : AggregateRoot<Customer>, IRolePlayer
    {
        public Customer()
        {
            RegisterEvent<NewCustomerCreatedEvent>(OnNewCustomerCreated);
            RegisterEvent<CashBalanceDecreasedEvent>(OnCashBalanceDecreased);
        }
            

        public string name { get; private set; }

        public decimal cashBalance { get; private set; }

        public void CreateNewCustomer(string name, decimal cashBalance)
        {
            var e = new NewCustomerCreatedEvent(name, cashBalance);
            Apply<NewCustomerCreatedEvent>(e)
                .SaveReport<CustomerDetailReport>(x=>
                    {
                        x.cashBalance = cashBalance;
                        x.name = name;
                    });
        }

        private void OnNewCustomerCreated(NewCustomerCreatedEvent e)
        {
            if (e.Name != null)
                this.name = e.Name;

            this.cashBalance = e.CashBalance;
        }

        public bool HasSufficientAmountToPurchase(decimal? totalAmount)
        {
            if (cashBalance >= totalAmount.Value)
                return true;
            return false;
        }

        public void DecreaseCashBalance(decimal totalAmount)
        {
            if (cashBalance >= totalAmount)
            {
                cashBalance -= totalAmount;
                var e = new CashBalanceDecreasedEvent { CashBalance = cashBalance };
                Apply<CashBalanceDecreasedEvent>(e)
                    .UpdateReport<CustomerDetailReport>(r => r.cashBalance = cashBalance);
            }
            else
            {
                throw new Exception("Insufficent amount to deduct");
            }
        }

        private void OnCashBalanceDecreased(CashBalanceDecreasedEvent e)
        {
            this.cashBalance = e.CashBalance;
        }
    }
}