using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Account.Value
{
    public class FundReservation : ValueObject
    {
        public FundReservation(Guid authorisationId, decimal amount)
        {
            AuthorisationId = authorisationId;
            Amount = amount;
        }

        public Guid AuthorisationId { get; }
        public decimal Amount { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AuthorisationId;
            yield return Amount;
        }
    }
}
