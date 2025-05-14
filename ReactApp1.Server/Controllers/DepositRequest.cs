namespace ReactApp1.Server.Controllers
{
    public class DepositRequest
    {
        public decimal Amount { get; set; }
        public required string Note { get; set; }
    }
}
