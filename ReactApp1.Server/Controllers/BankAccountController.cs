using Microsoft.AspNetCore.Mvc;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(ILogger<BankAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBankAccount")]
        public IEnumerable<BankAccount> Get()
        {
            _logger.LogInformation("Creating a new bank account for John Doe with an initial balance of 1000.");
            var account = new BankAccount("John Doe", 1000);

            _logger.LogInformation("Making a deposit of 500.");
            account.MakeDeposit(500, DateTime.Now, "Deposit");

            _logger.LogInformation("Making a withdrawal of 200.");
            account.MakeWithdrawal(200, DateTime.Now, "Withdrawal");

            _logger.LogInformation("Returning the bank account details");
            return new List<BankAccount> { account };
        }
    }
}
