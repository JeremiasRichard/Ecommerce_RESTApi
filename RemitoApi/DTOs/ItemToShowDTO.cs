namespace RemitoApi.DTOs
{
    public class ItemToShowDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DeliveryNoteId { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
