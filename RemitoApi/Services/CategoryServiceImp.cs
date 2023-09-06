using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemitoApi.DTOs;
using RemitoApi.Entities;
using RemitoApi.Exceptions;
using RemitoApi.Helpers;
using RemitoApi.Interfaces;
using RemitoApi.Repositories;

namespace RemitoApi.Services
{
    public class CategoryServiceImp : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServiceImp(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Category CreateCategory(CategoryCreateDTO createDTO)
        {

            if (_categoryRepository.GetByName(createDTO.Name) != null)
            {
                throw new CustomException("Category already exists!", 400);
            }

            var categoryMap = _mapper.Map<Category>(createDTO);

            if (_categoryRepository.Create(categoryMap) == null)
            {
                throw new CustomException("Something went wrong while saving", 500);
            }

            _categoryRepository.Save();
            return categoryMap;
        }

        public List<CategoryToShowDTO> GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryToShowDTO>>(_categoryRepository.GetAll());

            if (categories == null)
            {
                throw new NotFoundException("Category not found", 404);
            }

            return categories;
        }

        public IQueryable<Category> GetQueryable()
        {
            return _categoryRepository.GetAll();
        }

        public async Task<ActionResult<List<CategoryToShowDTO>>> GetAll(PaginationDTO paginationDTO)
        {
            var categories = await _categoryRepository.GetAll().ToPaginate(paginationDTO).ToListAsync();

            return _mapper.Map<List<CategoryToShowDTO>>(categories);
        }

        public CategoryToShowDTO GetAllProductsByCategory(int categoryId)
        {
            if (!_categoryRepository.Exist(categoryId))
            {
                throw new CustomException("Category not found", 404);
            }

            return _mapper.Map<CategoryToShowDTO>(_categoryRepository.GetById(categoryId));
        }
    }
}
