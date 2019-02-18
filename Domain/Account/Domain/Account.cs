using System;
using System.Collections.Generic;
using NLog;
using Domain.Account.Value;
using Domain.Common;

namespace Domain.Account.Domain
{
    public class Account : EventSourcedRootEntity, ICommandHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        //readonly IList<FundReservation> m_pendingTransactions;
        readonly AccountState m_state;

        //public decimal Fund { get; private set; }
        //public decimal PendingFund { get; private set; }
        public AccountId Id { get; private set; }

        public Account(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : base(eventStream, streamVersion)
        {
            // hydrate
            m_state = new AccountState(eventStream);
        }

        public void Create(AccountId id, decimal minBalance)
        {
            if (m_state.Created)
                throw new InvalidOperationException($"Account {id} already exists");
            Id = id;
            Apply(new AccountAdded(Id, minBalance, new GBPCurrency()));
            //m_pendingTransactions = new List<FundReservation>();
        }

        public bool CreditAccount(decimal credit)
        {
            if (m_state.Created)
            {
                Apply(new AccountCredited(m_state.Fund + credit));
                return true;
            }

            return false;
        }

        public bool Withdraw(decimal debit)
        {
            if (m_state.Created)
            {
                if (!IsDeficit(debit))
                {
                    Apply(new AccountDebited(m_state.Fund - debit));
                    return true;
                }

                Apply(new AccountDebitFailed(debit));
                return false;
            }

            return false;
        }

        internal bool IsDeficit(decimal amountRequest)
        {
            return amountRequest <= (m_state.Fund + m_state.MinBalance);
        }

        public bool CurrencyChanged(Currency newCurrency)
        {
            if (m_state.Created)
            {
                Apply(new CurrencyChanged(newCurrency));
                return true;
            }

            return false;
        }

        //public bool Reserve(decimal amountRequest)
        //{
        //    if (IsDeficit(amountRequest))
        //    {
        //        return false;
        //    }

        //    PendingFund += amountRequest;

        //    return true;
        //}

        public void Authorise(FundReservation reservation)
        {
            // get the reserve amount
            // Fire off request to Fund Authorisation component to another microservice
        }

        //public void OnFundRequestGranted(FundReservationReply reply)
        //{
        //    if (m_pendingTransactions.Remove(reply.FundReservation))
        //    {
        //        // update fund
        //        Fund += reply.FundReservation.Amount;
        //    }
        //    else
        //    {
        //        Log.Error($"Unable to find Fund Reservation [Id={reply.FundReservation.AuthorisationId} Amount={reply.FundReservation.Amount}]");
        //    }
        //}

        //public void OnFundRequestRevoke(FundReservationReply reply)
        //{
        //    if (m_pendingTransactions.Remove(reply.FundReservation))
        //    {
        //        Log.Error(reply.ErrorMsg);
        //    }
        //}

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Id;
        }
    }
}
