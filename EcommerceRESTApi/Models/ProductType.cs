using System.ComponentModel.DataAnnotations;

namespace EcommerceRESTApi.Entities
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
