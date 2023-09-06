using RemitoApi.Entities;

namespace RemitoApi.DTOs
{
    public class ProductCreateDTO
    {
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductOriginId { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool HasStock { get; set; }
    }
}
