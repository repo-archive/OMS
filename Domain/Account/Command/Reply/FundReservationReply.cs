using Domain.Account.Value;

namespace Domain.Account.Domain
{
    public class FundReservationReply : ReplyBase
    {

        public FundReservationReply(FundReservation fundReservation)
        {
            FundReservation = fundReservation;
        }

        public FundReservation FundReservation { get; }
    }
}