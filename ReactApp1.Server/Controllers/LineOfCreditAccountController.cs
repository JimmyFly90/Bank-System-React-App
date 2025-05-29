using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineOfCreditAccountController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, LineOfCreditAccount> _accounts = new();
        private static readonly ConcurrentDictionary<string, List<LogEntry>> _logs = new();

        private readonly ILogger<LineOfCreditAccountController> _logger;

        public LineOfCreditAccountController(ILogger<LineOfCreditAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRequest request)
        {
            if (request.InitialBalance < 0)
                return BadRequest("Initial balance cannot be negative.");

            var account = new LineOfCreditAccount(request.Owner, request.InitialBalance, 0);
            _accounts[account.Number] = account;
            _logs[account.Number] = new List<LogEntry>();

            var logMessage = $"Created a new account for {request.Owner} with an initial balance of {request.InitialBalance}";
            _logger.LogInformation(logMessage);
            _logs[account.Number].Add(new LogEntry(logMessage));

            return CreatedAtAction(nameof(GetLogs), new { accountNumber = account.Number }, new
            {
                AccountNumber = account.Number,
                Owner = account.Owner,
                Balance = account.Balance,
                Logs = _logs[account.Number]
            });
        }

        [HttpGet("{accountNumber}/logs")]
        public IActionResult GetLogs(string accountNumber)
        {
            if (!_accounts.ContainsKey(accountNumber))
                return NotFound("Account not found.");

            return Ok(_logs[accountNumber]);
        }

        [HttpPost("{accountNumber}/payments")]
        public IActionResult Payment(string accountNumber,[FromBody] PaymentRequest request)
        {
            if (!_accounts.TryGetValue(accountNumber, out var account))
                return NotFound("Account not found.");

            account.MakePayment(request.Amount, DateTime.Now);
            var logMessage = $"Payment of {request.Amount} made to account {account.Number}";
            _logger.LogInformation(logMessage);
            _logs[accountNumber].Add(new LogEntry(logMessage));

            return Ok(new
            {
                Balance = account.Balance,
                Logs = _logs[accountNumber]
            });
        }

    }
}