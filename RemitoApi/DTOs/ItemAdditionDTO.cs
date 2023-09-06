using RemitoApi.Entities;
using RemitoApi.Models;

namespace RemitoApi.DTOs
{
    public class ItemAdditionDTO
    {
        public int ProductId { get; set; }
        public int DeliveryNoteId { get; set; }
        public int Quantity { get; set; }
    }
}
