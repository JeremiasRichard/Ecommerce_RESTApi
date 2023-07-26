using System.ComponentModel.DataAnnotations;

namespace RemitoApi.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryTypeId { get; set; }
        public  virtual CategoryType CategoryType { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}