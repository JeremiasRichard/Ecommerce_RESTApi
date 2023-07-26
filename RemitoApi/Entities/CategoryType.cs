using System.ComponentModel.DataAnnotations;

namespace RemitoApi.Entities
{
    public class CategoryType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
