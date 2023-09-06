using Microsoft.AspNetCore.Mvc;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.DTOs.Secutiry;
using EcommerceRESTApi.Entities;

namespace EcommerceRESTApi.Services
{
    public interface IProductOriginService
    {
        public ProductOrigin CreateProductOrigin(ProductOriginCreateDTO createDTO);
        public List<ProductOriginToShowDTO> GetAllProductsByOrigin(int id);
        public Task<ActionResult<List<ProductOriginToShowDTO>>> GetAll(PaginationDTO paginationDTO);

    }
}
