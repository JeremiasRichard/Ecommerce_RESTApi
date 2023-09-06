using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Entities;
using EcommerceRESTApi.Exceptions;
using EcommerceRESTApi.Helpers;
using EcommerceRESTApi.Interfaces;

namespace EcommerceRESTApi.Services
{
    public class ProductServiceImp : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IProductOriginRepository _productOriginRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductServiceImp(IProductRepository productRepository, IMapper mapper, IProductOriginRepository productOriginRepository, ICategoryRepository categoryRepository, IProductTypeRepository productTypeRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _productOriginRepository = productOriginRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ProductToShowDTO CreateProduct(ProductCreateDTO createDTO)
        {
            using (var transaction = _productRepository.BeginTransaction())
            {
                ValidateProductCreateDTO(createDTO);

                var category = _categoryRepository.GetById(createDTO.CategoryId);

                var productMap = _mapper.Map<Product>(createDTO);

                productMap.Category = category;

                var product = _productRepository.Create(productMap);
                _productRepository.Save();


                if (product == null)
                {
                    transaction.Rollback();
                    throw new CustomException("Something went wrong while saving", 500);
                }
                transaction.Commit();

                return _mapper.Map<ProductToShowDTO>(product);
            }
        }

        public IQueryable<Product> GetQueryable()
        {
            return _productRepository.GetAll();
        }

        public async Task<ActionResult<List<ProductToShowDTO>>> GetAll(PaginationDTO paginationDTO)
        {
            var products = await _productRepository.GetAll().ToPaginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<ProductToShowDTO>>(products);
        }

        public Product DeleteProduct(int productId)
        {
            var productToDelete = _productRepository.GetById(productId);

            if (productToDelete == null)
            {
                throw new CustomException("Product not found",404);
            }

            _productRepository.Remove(productToDelete);
            _productRepository.Save();
            return productToDelete;
        }

        public ProductCreateDTO Update(ProductCreateDTO product)
        {
            var productToUpdate = _mapper.Map<Product>(_productRepository.GetByNameOriginAndType(product.ProductName, product.ProductOriginId, product.ProductTypeId));
            _productRepository.Update(productToUpdate);
            return _mapper.Map<ProductCreateDTO>(productToUpdate);
        }

        public void ValidateProductCreateDTO(ProductCreateDTO createDTO)
        {
            if (!_categoryRepository.Exist(createDTO.CategoryId))
            {
                throw new NotFoundException("Category not found", 404);
            }

            if (!_productOriginRepository.Exist(createDTO.ProductOriginId))
            {
                throw new NotFoundException("Product Origin not found", 404);
            }

            if (!_productTypeRepository.Exist(createDTO.ProductTypeId))
            {
                throw new NotFoundException("Product Type not found", 404);
            }

            if (_productRepository.GetByNameOriginAndType(createDTO.ProductName, createDTO.ProductOriginId, createDTO.ProductTypeId))
            {
                throw new CustomException("Product already exists!", 400);
            }
        }
    }
}
