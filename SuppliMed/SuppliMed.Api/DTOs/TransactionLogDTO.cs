namespace SuppliMed.Api.DTOs
{
    public class TransactionLogDto
    {
        public string LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string User { get; set; }
        public string Action { get; set; }
        public string Item { get; set; }
        public string Details { get; set; }
    }
}