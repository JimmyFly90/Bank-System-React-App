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
                Timestamp = DateTime.Now.ToString("YYYY-MM-DD HH:MM:SS");
                Message = message;
            }
        }

        [HttpGet(Name = "GetBankAccount")]
        public IEnumerable<LogEntry> Get()
        {
            var logs = new List<LogEntry>();

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

        [HttpPost ("withdrawal")]
        public IActionResult Withdrawal([FromBody] WithdrawalRequest request)
        {
            var logs = new List<LogEntry>();

            var account = new BankAccount("John Doe", 1000);
            account.MakeWithdrawal(request.Amount, DateTime.Now, request.Note);
            var logMessage = $"Withdrawal of {request.Amount} from account {account.Number} with note: {request.Note}";
            _logger.LogInformation(logMessage);
            logs.Add(new LogEntry(logMessage));

            return Ok(new
            {
                Balance = account.Balance,
                Logs = logs
            });
        }

        public class WithdrawalRequest
        {
            public decimal Amount { get; set; }
            public required string Note { get; set; }
        }
    }
}