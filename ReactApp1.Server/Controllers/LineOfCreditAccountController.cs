using Microsoft.AspNetCore.Mvc;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineOfCreditAccountController : ControllerBase
    {
        private readonly ILogger<LineOfCreditAccountController> _logger;

        public LineOfCreditAccountController(ILogger<LineOfCreditAccountController> logger)
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

        [HttpGet(Name = "GetLineOfCreditAccount")]
        public IEnumerable<LogEntry> Get()
        {
            var logs = new List<LogEntry>();

            return logs;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRequest request)
        {
            var logs = new List<LogEntry>();

            if (request.InitialBalance > 0)
            {
                logs.Add(new LogEntry("Failed to create account: Initial balance cannot be positive."));
                return BadRequest("Initial balance cannot be positive.");
            }

            var account = new BankAccount(request.Owner, request.InitialBalance);
            var logMessage = $"Created a new Line of Credit account for {request.Owner} with an initial balance of {request.InitialBalance}";
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
            public decimal CreditLimit { get; set; }
        }

        [HttpPost("payment")]
        public IActionResult Payment([FromBody] PaymentRequest request)
        {
            var logs = new List<LogEntry>();

            var account = new LineOfCreditAccount("John Doe", 0, -1000);
            account.MakePayment(request.Amount, DateTime.Now);
            var logMessage = $"Payment of {request.Amount} made to account {account.Number}";
            _logger.LogInformation(logMessage);
            logs.Add(new LogEntry(logMessage));

            return Ok(new
            {
                Balance = account.Balance,
                Logs = logs
            });
        }

        public class PaymentRequest
        {
            public decimal Amount { get; set; }
        }
    }
}