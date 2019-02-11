namespace Domain.Account.Domain
{
    public class ReplyBase
    {

        public bool IsError 
        {
            get => !string.IsNullOrWhiteSpace(ErrorMsg);
        }

        public string ErrorMsg { get; set; }
    }
}