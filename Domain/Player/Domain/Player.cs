using System;
using System.Collections.Generic;
using Domain.Account.Domain;
using Domain.Common;
using Domain.Player.Domain;

namespace Sample.Domain
{
    public class Player : EventSourcedRootEntity
    {
        /// <summary> List of uncommitted changes </summary>
        public readonly IList<IDomainEvent> Changes = new List<IDomainEvent>();

        readonly PlayerState _state;

        public Player(IEnumerable<IDomainEvent> events)
        {
            _state = new PlayerState(events);
        }

        void Apply(IEvent e)
        {
            // pass each event to modify current in-memory state
            _state.Mutate(e);
            // append event to change list for further persistence
            Changes.Add(e);
        }


        public void Create(PlayerId id, string name)//DateTime utc)
        {
            if (_state.Created)
                throw new InvalidOperationException("Player already exist");
            Apply(new PlayerCreated
            {
                Created = DateTimeOffset.UtcNow,
                Name = name,
                Id = id,
            });

            var bonus = service.GetWelcomeBonus(currency);
            if (bonus.Amount > 0)
                AddPayment("Welcome bonus", bonus, utc);
        }
        public void Rename(string name, DateTime dateTime)
        {
            if (_state.Name == name)
                return;
            Apply(new CustomerRenamed
            {
                Name = name,
                Id = _state.Id,
                OldName = _state.Name,
                Renamed = dateTime
            });
        }

        public void LockCustomer(string reason)
        {
            if (_state.ConsumptionLocked)
                return;

            Apply(new CustomerLocked
            {
                Id = _state.Id,
                Reason = reason
            });
        }

        public void LockForAccountOverdraft(string comment, IPricingService service)
        {
            if (_state.ManualBilling) return;
            var threshold = service.GetOverdraftThreshold(_state.Currency);
            if (_state.Balance < threshold)
            {
                LockCustomer("Overdraft. " + comment);
            }

        }

        public void AddPayment(string name, CurrencyAmount amount, DateTime utc)
        {
            Apply(new CustomerPaymentAdded()
            {
                Id = _state.Id,
                Payment = amount,
                NewBalance = _state.Balance + amount,
                PaymentName = name,
                Transaction = _state.MaxTransactionId + 1,
                TimeUtc = utc
            });
        }

        public void Charge(string name, CurrencyAmount amount, DateTime time)
        {
            if (_state.Currency == Currency.None)
                throw new InvalidOperationException("Customer currency was not assigned!");
            Apply(new CustomerChargeAdded()
            {
                Id = _state.Id,
                Charge = amount,
                NewBalance = _state.Balance - amount,
                ChargeName = name,
                Transaction = _state.MaxTransactionId + 1,
                TimeUtc = time
            });
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            throw new NotImplementedException();
        }
    }
}