using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaccine.Core;
using VaccineWeb.Preview.Models.Events;
using VaccineWeb.Preview.Models.Events.Customers;
using VaccineWeb.Preview.Models.Reports.Customers;
using Vaccine.Core.Events;

namespace VaccineWeb.Preview.Models.Domains.Customers
{
    public class Customer : AggregateRoot<Customer>, IRolePlayer
    {
        public Customer()
        {
            RegisterEvent<NewCustomerCreatedEvent>(OnNewCustomerCreated);
            RegisterEvent<CashBalanceDecreasedEvent>(OnCashBalanceDecreased);
            RegisterEvent<CashBalanceIncreasedEvent>(OnCashBalanceIncreased);
        }
            

        public string name { get; private set; }

        public decimal cashBalance { get; private set; }

        public void CreateNewCustomer(string name, decimal cashBalance)
        {
            var e = new NewCustomerCreatedEvent(name, cashBalance);
            Apply<NewCustomerCreatedEvent>(e)
                .Save<CustomerDetailReport>(x =>
                {
                    x.AggregateRootId = e.AggregateRootId;
                    x.name = e.Name;
                    x.cashBalance = e.CashBalance;
                }
            );
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
                    .Update<CustomerDetailReport>(r => r.cashBalance = cashBalance);
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

        public void IncreaseCashBalance(decimal totalAmount)
        {
            cashBalance += totalAmount;
            var e = new CashBalanceIncreasedEvent { CashBalance = cashBalance};
            Apply<CashBalanceIncreasedEvent>(e)
                .Update<CustomerDetailReport>(x => x.cashBalance = cashBalance);
        }

        private void OnCashBalanceIncreased(CashBalanceIncreasedEvent e)
        {
            cashBalance = e.CashBalance;
        }
    }
}