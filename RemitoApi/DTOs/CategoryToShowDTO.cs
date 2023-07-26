using RemitoApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace RemitoApi.DTOs
{
    public class CategoryToShowDTO
    {
        public int Id { get; set; }
        public int CategoryTypeId { get; set; }
        public virtual string CategoryTypeName { get; set; }
        public virtual ICollection<ProductToShowDTO> Products { get; set; }
    }
}
