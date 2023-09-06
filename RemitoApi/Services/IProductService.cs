﻿using Microsoft.AspNetCore.Mvc;
using RemitoApi.DTOs;
using RemitoApi.Entities;

namespace RemitoApi.Services
{
    public interface IProductService
    {
        public ProductToShowDTO CreateProduct(ProductCreateDTO createDTO);
        public IQueryable<Product> GetQueryable();
        public Task<ActionResult<List<ProductToShowDTO>>> GetAll(PaginationDTO paginationDTO);
        public Product DeleteProduct(int productId);
        public ProductCreateDTO Update(ProductCreateDTO product);
        public void ValidateProductCreateDTO(ProductCreateDTO createDTO);
    }
}
