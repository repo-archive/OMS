using Domain.Common;
using System.Collections.Generic;

namespace Domain.Account.Domain
{
    public class AccountState
    {
        public AccountId Id { get; private set; }
        public bool Created { get; private set; }
        public decimal Fund { get; private set; }
        public decimal MinBalance { get; private set; }
        public Currency Currency { get; private set; }

        public AccountState(IEnumerable<IDomainEvent> events)
        {
            foreach (var e in events)
            {
                Mutate(e);
            }
        }

        public void Mutate(IDomainEvent e)
        {
            ((dynamic)this).When((dynamic)e);
        }

        public void When(AccountAdded e)
        {
            MinBalance = e.NewBalance;
            Fund = e.NewBalance;
            Currency = e.Currency;
            Created = true;
        }

        public void When(AccountCredited e)
        {
            if (!Created)
                Fund = e.NewBalance;
        }

        public void When(AccountDebited e)
        {
            if (!Created)
                Fund = e.NewBalance;
        }

        //public void When(CustomerCreated e)
        //{
        //    Created = true;
        //    Name = e.Name;
        //    Id = e.Id;
        //    Currency = e.Currency;
        //    Balance = new CurrencyAmount(0, e.Currency);
        //}

        public void When(CurrencyChanged e)
        {
            Currency = e.NewCurrency;
        }
    }
}
