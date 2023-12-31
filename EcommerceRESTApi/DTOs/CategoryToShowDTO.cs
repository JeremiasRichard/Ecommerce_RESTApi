﻿using EcommerceRESTApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace EcommerceRESTApi.DTOs
{
    public class CategoryToShowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductToShowDTO> Products { get; set; }
    }
}
