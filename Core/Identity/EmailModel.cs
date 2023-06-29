namespace Core.Identity
{
    public class EmailModel
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public EmailModel(string to, string sub,string body)
        {
            To = to;
            Subject= sub;
            Body= body;
        }
    }
}
