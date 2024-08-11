namespace ReturnOrdersApi.Model
{
    public class ReturnOrder
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public string Reason { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ProcessedDate { get; set; }
    }
}
