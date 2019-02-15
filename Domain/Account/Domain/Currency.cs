using Domain.Common;
using System.Collections.Generic;

namespace Domain.Account.Domain
{
    public class Currency : ValueObject
    {
        public int Id;
        public string Name;

        public Currency()
        {
            Id = -1;
            Name = string.Empty;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }

    public class NoneCurrency : Currency
    {
        public NoneCurrency()
        {
            Id = -1;
            Name = "None";
        }
    }

    public class GBPCurrency : Currency
    {
        public GBPCurrency()
        {
            Id = 1;
            Name = "GBP";
        }
    }

    public class USDCurrency : Currency
    {
        public USDCurrency()
        {
            Id = 2;
            Name = "USD";
        }
    }
}
