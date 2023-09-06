using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemitoApi.DTOs;
using RemitoApi.DTOs.Secutiry;
using RemitoApi.Entities;
using RemitoApi.Exceptions;
using RemitoApi.Helpers;
using RemitoApi.Interfaces;
using RemitoApi.Repositories;

namespace RemitoApi.Services
{
    public class ProductOriginServiceImp : IProductOriginService
    {

        private readonly IProductOriginRepository _productOriginRepository;
        private readonly IMapper _mapper;

        public ProductOriginServiceImp(IProductOriginRepository productOriginRepository, IMapper mapper)
        {
            _productOriginRepository = productOriginRepository;
            _mapper = mapper;
        }

        public ProductOrigin CreateProductOrigin(ProductOriginCreateDTO createDTO)
        {
            var productOrigin = _productOriginRepository.GetAll()
               .Where(x => x.Name.Trim().ToUpper() == createDTO.Name.TrimEnd().ToUpper())
               .FirstOrDefault();

            if (productOrigin != null)
            {
                throw new CustomException("Product origin already exist!", 400);
            }

            var productOriginMap = _mapper.Map<ProductOrigin>(createDTO);
            if (_productOriginRepository.Create(productOriginMap) == null)
            {
                throw new CustomException("Something went wrong while saving", 500);
            }

            _productOriginRepository.Save();

            return productOriginMap;
        }

        public IQueryable<ProductOrigin> GetAllProductOrigin()
        {
            return _productOriginRepository.GetAll();
        }


        public async Task<ActionResult<List<ProductOriginToShowDTO>>>GetAll(PaginationDTO paginationDTO)
        {
            var productOrigins = await _productOriginRepository.GetAll().ToPaginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<ProductOriginToShowDTO>>(productOrigins);
        }

        public List<ProductOriginToShowDTO> GetAllProductsByOrigin(int id)
        {   
            if(!_productOriginRepository.Exist(id))
            {
                throw new CustomException("Product origin doesn't exist!", 404);
            }

            return _mapper.Map<List<ProductOriginToShowDTO>>(_productOriginRepository.GetAll().Where(x => x.Id == id)
                .Include(c => c.Products)
                .ThenInclude(c => c.ProductType));
        }
    }
}
