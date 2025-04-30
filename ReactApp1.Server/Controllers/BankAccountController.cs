using Microsoft.AspNetCore.Mvc;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateRequest request)
        {
            if (request.InitialBalance < 0)
            {
                return BadRequest("Initial balance cannot be negative.");
            }

            var account = new BankAccount(request.Owner, request.InitialBalance);
            _logger.LogInformation($"Created a new bank account for {request.Owner} with an initial balance of {request.InitialBalance}");

            return Ok(new
            {
                AccountNumber = account.Number,
                Owner = account.Owner,
                Balance = account.Balance
            });
        }

        public class CreateRequest
        {
            public required string Owner { get; set; }
            public decimal InitialBalance { get; set; }
        }

        [HttpPost ("deposit")]
        public IActionResult Deposit([FromBody] DepositRequest request)
        {
            var account = new BankAccount("John Doe", 1000);
            account.MakeDeposit(request.Amount, DateTime.Now, request.Note);
            return Ok(new { Balance = account.Balance });
        }

        public class DepositRequest
        {
            public decimal Amount { get; set; }
            public required string Note { get; set; }
        }
    }
}