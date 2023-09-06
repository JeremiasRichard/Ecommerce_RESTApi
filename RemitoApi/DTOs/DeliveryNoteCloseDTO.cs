using RemitoApi.Models;

namespace RemitoApi.DTOs
{
    public class DeliveryNoteCloseDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<ItemToShowDTO> ItemsProducts { get; set; }
        public double Total { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
