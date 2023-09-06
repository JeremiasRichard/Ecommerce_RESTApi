using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.Models
{
    public class DeliveryNote
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Items> ItemsProducts { get; set; }
        public double Total { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsOpen { get; set; }
    }
}
