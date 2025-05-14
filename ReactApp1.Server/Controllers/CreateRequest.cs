namespace ReactApp1.Server.Controllers
{
    public class CreateRequest
    {
        public required string Owner { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
