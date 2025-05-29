using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterestEarningAccountController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, InterestEarningAccount> _accounts = new();
        private static readonly ConcurrentDictionary<string, List<LogEntry>> _logs = new();

        private readonly ILogger<InterestEarningAccountController> _logger;

        public InterestEarningAccountController(ILogger<InterestEarningAccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRequest request)
        {
            if (request.InitialBalance < 0)
                return BadRequest("Initial balance cannot be negative.");
            
            var account = new InterestEarningAccount(request.Owner, request.InitialBalance);
            _accounts[account.Number] = account;
            _logs[account.Number] = new List<LogEntry>();

            var logMessage = $"Created a new bank account for {request.Owner} with an initial balance of {request.InitialBalance}";
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

        [HttpPost("{accountNumber}/deposits")]
        public IActionResult Deposit(string accountNumber, [FromBody] DepositRequest request)
        {
            if (!_accounts.TryGetValue(accountNumber, out var account))
                return NotFound("Account not found.");

            account.MakeDeposit(request.Amount, DateTime.Now, request.Note);
            var logMessage = $"Deposited {request.Amount} to account {account.Number} with note: {request.Note}";
            _logger.LogInformation(logMessage);
            _logs[accountNumber].Add(new LogEntry(logMessage));

            return Ok(new
            {
                Balance = account.Balance,
                Logs = _logs[accountNumber]
            });
        }

        [HttpPost("{accountNumber}/withdrawals")]
        public IActionResult Withdrawal(string accountNumber, [FromBody] WithdrawalRequest request)
        {
            if (!_accounts.TryGetValue(accountNumber, out var account))
                return NotFound("Account not found.");

            account.MakeWithdrawal(request.Amount, DateTime.Now, request.Note);
            var logMessage = $"Withdrawal of {request.Amount} from account {account.Number} with note: {request.Note}";
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