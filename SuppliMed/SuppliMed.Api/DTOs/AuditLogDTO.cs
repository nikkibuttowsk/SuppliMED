namespace SuppliMed.Api.DTOs
{
    public class AuditLogDto
    {
        public string LogId { get; set; } = default!;
        public DateTime DateTime { get; set; } 
        public string User { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Item { get; set; } = default!;
        public string Details { get; set; } = default!;
    }
}