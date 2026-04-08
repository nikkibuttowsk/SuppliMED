namespace AppCore.Models
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }

        public string GetDetails()
        {
            return $"{TransactionID} | {Type} | {Quantity} | {Date}";
        }
    }
}