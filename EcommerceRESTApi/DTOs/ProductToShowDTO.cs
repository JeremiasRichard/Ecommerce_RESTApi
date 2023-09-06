using EcommerceRESTApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace EcommerceRESTApi.DTOs
{
    public class ProductToShowDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductOriginId { get; set; }
        public string ProductOriginName { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
