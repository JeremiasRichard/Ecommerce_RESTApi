using RemitoApi.Models;
using System.ComponentModel.DataAnnotations;

namespace RemitoApi.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int ProductOriginId { get; set; }
        public virtual ProductOrigin ProductOrigin { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool HasStock { get; set; }
        public virtual ICollection<Items> Items { get; set; }
    }
}
