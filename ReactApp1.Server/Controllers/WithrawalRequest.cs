namespace ReactApp1.Server.Controllers
{
    public class WithdrawalRequest
    {
        public decimal Amount { get; set; }
        public required string Note { get; set; }
    }
}
