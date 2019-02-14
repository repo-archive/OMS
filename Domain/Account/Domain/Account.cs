using System;
using System.Collections.Generic;
using NLog;
using Domain.Account.Value;
using Domain.Common;

namespace Domain.Account.Domain
{
    public class AccountId : Identity

    {
        public AccountId(string id)
        {
            Id = id;
        }
    }

    public class Account : Entity, ICommandHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        readonly int m_customerId;
        readonly decimal m_min;
        readonly IList<FundReservation> m_pendingTransactions;

        public decimal Fund { get; private set; }
        public decimal PendingFund { get; private set; }
        public AccountId Id { get; private set; }

        public Account Create(int customerId, decimal minBalance)
        {
            m_min = minBalance;
            Id = new AccountId(Guid.NewGuid().ToString());
            m_pendingTransactions = new List<FundReservation>();

            return Id;
        }

        public void Add(decimal credit)
        {
            PendingFund += credit;
        }

        public bool Withdraw(decimal debit)
        {
            if (!IsDeficit(debit))
            {
                Fund -= debit;
                return true;
            }

            return false;
        }

        internal bool IsDeficit(decimal amountRequest)
        {
            return amountRequest <= (Fund + m_min);
        }

        public bool Reserve(decimal amountRequest)
        {
            if (IsDeficit(amountRequest))
            {
                return false;
            }

            PendingFund += amountRequest;

            return true;
        }

        public void Authorise(FundReservation reservation)
        {
            // get the reserve amount
            // Fire off request to Fund Authorisation component to another microservice
        }

        public void OnFundRequestGranted(FundReservationReply reply)
        {
            if (m_pendingTransactions.Remove(reply.FundReservation))
            {
                // update fund
                Fund += reply.FundReservation.Amount;
            }
            else
            {
                Log.Error($"Unable to find Fund Reservation [Id={reply.FundReservation.AuthorisationId} Amount={reply.FundReservation.Amount}]");
            }
        }

        public void OnFundRequestRevoke(FundReservationReply reply)
        {
            if (m_pendingTransactions.Remove(reply.FundReservation))
            {
                Log.Error(reply.ErrorMsg);
            }
        }
    }
}
