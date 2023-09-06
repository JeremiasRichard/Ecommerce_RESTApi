using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemitoApi.DTOs;
using RemitoApi.Entities;
using RemitoApi.Exceptions;
using RemitoApi.Helpers;
using RemitoApi.Interfaces;

namespace RemitoApi.Services
{
    public class ProductTypeServiceImp : IProductTypeService
    {

        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeServiceImp(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public ProductType CreateProductType(ProductTypeCreateDTO createDTO)
        {

            var productType = _productTypeRepository.GetAll()
               .Where(x => x.Name.Trim().ToUpper() == createDTO.Name.TrimEnd().ToUpper())
               .FirstOrDefault();

            if (productType != null)
            {
                throw new CustomException("Product Type already exist!", 400);
            }

            var productTypeMap = _mapper.Map<ProductType>(createDTO);

            var p = _productTypeRepository.Create(productTypeMap);

            if (p == null)
            {
                throw new CustomException("Something went wrong while saving", 500);
            }
            _productTypeRepository.Save();
            return p;
        }

        public async Task<ActionResult<List<ProductTypeToShowDTO>>> GetAll(PaginationDTO paginationDTO)
        {
            var productOrigins = await _productTypeRepository.GetAll().ToPaginate(paginationDTO).ToListAsync();

            return _mapper.Map<List<ProductTypeToShowDTO>>(productOrigins);
        }

        public IQueryable<ProductType> GetQueryable()
        {
            return _productTypeRepository.GetAll();
        }

        public List<ProductTypeToShowDTO> GetAllProductsByType(int id)
        { 
            if(!_productTypeRepository.Exist(id))
            {
                throw new CustomException("Product type doesn't exist!",404);
            }

            return _mapper.Map<List<ProductTypeToShowDTO>>(_productTypeRepository.GetAll().Where(x => x.Id == id));
        }


    }
}
