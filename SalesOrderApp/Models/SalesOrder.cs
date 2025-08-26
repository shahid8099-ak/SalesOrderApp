namespace SalesOrderApp.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string SalesOrderNo { get; set; }
     
        public string CustomerId { get; set; }
        public string PaymentTerms { get; set; }

        public List<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
    }
}