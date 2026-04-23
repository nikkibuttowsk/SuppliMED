namespace AppCore.Models
{
    public class Transaction
    {
        public required string TransactionID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public required string Type { get; set; }
        public required string AddedBy { get; set; }

        public string GetDetails()
        {
            return $"{TransactionID} | {Type} | {Quantity} | {Date}";
        }
    }
}