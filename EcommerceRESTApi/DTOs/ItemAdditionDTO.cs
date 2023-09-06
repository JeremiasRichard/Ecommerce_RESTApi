using EcommerceRESTApi.Entities;
using EcommerceRESTApi.Models;

namespace EcommerceRESTApi.DTOs
{
    public class ItemAdditionDTO
    {
        public int ProductId { get; set; }
        public int DeliveryNoteId { get; set; }
        public int Quantity { get; set; }
    }
}
