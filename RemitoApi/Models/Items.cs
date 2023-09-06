using RemitoApi.Entities;

namespace RemitoApi.Models
{
    public class Items
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DeliveryNoteId { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public virtual Product Product { get; set; }
        public virtual DeliveryNote DeliveryNote { get; set; }
    }
}
