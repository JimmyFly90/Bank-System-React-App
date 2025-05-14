namespace ReactApp1.Server.Controllers
{
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
}
