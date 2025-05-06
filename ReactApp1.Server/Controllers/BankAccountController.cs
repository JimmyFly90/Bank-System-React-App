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

        public class LogEntry
        {
            public string Timestamp { get; set; }
            public string Message { get; set; }

            public LogEntry(string message)
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Message = message;
            }
        }

        [HttpGet(Name = "GetBankAccount")]
        public IEnumerable<LogEntry> Get()
        {
            var logs = new List<LogEntry>();

            _logger.LogInformation("Creating a new bank account for John Doe with an initial balance of 1000.");
            logs.Add(new LogEntry("Creating a new bank account for John Doe with an initial balance of 1000."));
            var account = new BankAccount("John Doe", 1000);

            _logger.LogInformation("Making a deposit of 500.");
            logs.Add(new LogEntry("Making a deposit of 500."));
            account.MakeDeposit(500, DateTime.Now, "Deposit");

            _logger.LogInformation("Making a withdrawal of 200.");
            logs.Add(new LogEntry("Making a withdrawal of 200."));
            account.MakeWithdrawal(200, DateTime.Now, "Withdrawal");

            _logger.LogInformation("Returning the bank account details");
            logs.Add(new LogEntry("Returning the bank account details"));

            return logs;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRequest request)
        {
            var logs = new List<LogEntry>();

            if (request.InitialBalance < 0)
            {
                logs.Add(new LogEntry("Failed to create account: Initial balance cannot be negative."));
                return BadRequest("Initial balance cannot be negative.");
            }

            var account = new BankAccount(request.Owner, request.InitialBalance);
            var logMessage = $"Created a new bank account for {request.Owner} with an initial balance of {request.InitialBalance}";
            _logger.LogInformation(logMessage);
            logs.Add(new LogEntry(logMessage));

            return Ok(new
            {
                AccountNumber = account.Number,
                Owner = account.Owner,
                Balance = account.Balance,
                Logs = logs
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
            var logs = new List<LogEntry>();

            var account = new BankAccount("John Doe", 1000);
            account.MakeDeposit(request.Amount, DateTime.Now, request.Note);
            var logMessage = $"Deposited {request.Amount} to account {account.Number} with note: {request.Note}";
            _logger.LogInformation(logMessage);
            logs.Add(new LogEntry(logMessage));

            return Ok(new 
            { 
                Balance = account.Balance,
                Logs = logs
            });
        }

        public class DepositRequest
        {
            public decimal Amount { get; set; }
            public required string Note { get; set; }
        }
    }
}