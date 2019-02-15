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
}
