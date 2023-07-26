using System.ComponentModel.DataAnnotations;

namespace RemitoApi.Entities
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
    }
}
