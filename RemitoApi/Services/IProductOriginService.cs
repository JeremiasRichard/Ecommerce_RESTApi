using Microsoft.AspNetCore.Mvc;
using RemitoApi.DTOs;
using RemitoApi.DTOs.Secutiry;
using RemitoApi.Entities;

namespace RemitoApi.Services
{
    public interface IProductOriginService
    {
        public ProductOrigin CreateProductOrigin(ProductOriginCreateDTO createDTO);
        public List<ProductOriginToShowDTO> GetAllProductsByOrigin(int id);
        public Task<ActionResult<List<ProductOriginToShowDTO>>> GetAll(PaginationDTO paginationDTO);

    }
}
