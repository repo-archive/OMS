using System;
using Domain.Common;

namespace Domain.Account.Domain
{
    public class AccountAdded : IDomainEvent
    {
        public decimal NewBalance { get; }
        public Currency Currency { get; }
        public DateTimeOffset OccuredOn { get; }

        public AccountAdded(decimal newBalance, Currency currency)
        {
            NewBalance = newBalance;
            Currency = currency;
            OccuredOn = DateTimeOffset.UtcNow;
        }
    }

    public class AccountCredited : IDomainEvent
    {
        public decimal NewBalance { get; }
        public DateTimeOffset OccuredOn { get; }

        public AccountCredited(decimal newBalance)
        {
            NewBalance = newBalance;
            OccuredOn = DateTimeOffset.UtcNow;
        }
    }

    public class AccountDebited : IDomainEvent
    {
        public decimal NewBalance { get; }
        public DateTimeOffset OccuredOn { get; }

        public AccountDebited(decimal newBalance)
        {
            NewBalance = newBalance;
            OccuredOn = DateTimeOffset.UtcNow;
        }
    }

    public class AccountDebitFailed : IDomainEvent
    {
        public decimal NewBalance { get; }
        public DateTimeOffset OccuredOn { get; }

        public AccountDebitFailed(decimal newBalance)
        {
            NewBalance = newBalance;
            OccuredOn = DateTimeOffset.UtcNow;
        }
    }

    public class CurrencyChanged : IDomainEvent
    {
        public Currency NewCurrency { get; }
        public DateTimeOffset OccuredOn { get; }

        public CurrencyChanged(Currency currency)
        {
            NewCurrency = currency;
            OccuredOn = DateTimeOffset.UtcNow;
        }
    }
}