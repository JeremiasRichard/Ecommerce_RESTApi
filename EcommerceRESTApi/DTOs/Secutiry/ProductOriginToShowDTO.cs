using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.DTOs.Secutiry
{
    public class ProductOriginToShowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductToShowDTO> Products { get; set; }
    }
}
