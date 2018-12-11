using System;
using Domain.Common;

namespace Domain.Account
{
    public class AccountId
    {
        public string Id { get; private set; }
        public AccountId(string id)
        {
            Id = id;
        }
    }

    public class Account : EntityBase, ICommandHandler
    {
        readonly int m_customerId;
        readonly int m_min;

        public int Fund { get; private set; }
        public AccountId Id { get; private set; }

        public Account(int customerId, int minBalance)
        {
            m_min = minBalance;
            Id = new AccountId(Guid.NewGuid().ToString());
        }

        public void Add(int fund)
        {
            Fund += fund;
        }

        public void Withdraw(int fund)
        {
            Fund -= fund;
        }

        public bool IsFundOver(int requestAmount)
        {
            return requestAmount <= (Fund + m_min);
        }

        public void 
    }
}
