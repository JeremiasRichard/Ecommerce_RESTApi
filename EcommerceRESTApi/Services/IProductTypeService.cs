using Microsoft.AspNetCore.Mvc;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.Services
{
    public interface IProductTypeService
    {
        public ProductType CreateProductType(ProductTypeCreateDTO createDTO);
        public Task<ActionResult<List<ProductTypeToShowDTO>>> GetAll(PaginationDTO paginationDTO);
        public IQueryable<ProductType> GetQueryable();
        public List<ProductTypeToShowDTO> GetAllProductsByType(int id);
    }
}
